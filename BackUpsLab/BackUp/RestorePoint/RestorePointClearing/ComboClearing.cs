using System;
using System.Collections.Generic;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class ComboClearing : Interfaces.RestorePointClearing
    {
        private Interfaces.RestorePointClearing _resultClearing;
        private readonly List<IRestorePointCount> _availableLimits = new List<IRestorePointCount>();
        private readonly ComboType _type;

        public ComboClearing(ComboType type, DateTime time, int count, long size)
        {
            _type = type;
            ClearingTime = time;
            ClearingCount = count;
            ClearingSize = size;
        }

        public enum ComboType
        {
            Max,
            Min
        }
        
        public override void ClearRestorePoint(BackUp backUp)
        {
            FindResultCleaning(backUp);
            _resultClearing.ClearRestorePoint(backUp);
        }

        private bool CheckCount() => ClearingCount != int.MaxValue;
        private bool CheckTime() => ClearingTime != DateTime.MaxValue;
        private bool CheckSize() => ClearingSize != long.MaxValue;
        private void FindResultCleaning(BackUp backUp)
        {
            AddAvailableLimits();
            if (_type == ComboType.Max)
            {
                _resultClearing = FindMaxCleaning(backUp);
            }

            if (_type == ComboType.Min)
            {
                _resultClearing = FindMinCleaning(backUp);
            }
        }

        private Interfaces.RestorePointClearing FindMaxCleaning(BackUp backUp)
        {
            var maxCount = int.MinValue;
            Interfaces.RestorePointClearing maxCleaning = null;
            foreach (var limit in _availableLimits)
            {
                var count = limit.CountRestorePointsForCleaning(backUp);
                if (count > maxCount)
                {
                    maxCount = count;
                    maxCleaning = limit.ToRestore();
                }
            }

            return maxCleaning;
        }

        private Interfaces.RestorePointClearing FindMinCleaning(BackUp backUp)
        {
            var minCount = int.MaxValue;
            Interfaces.RestorePointClearing minCleaning = null;
            foreach (var limit in _availableLimits)
            {
                var count = limit.CountRestorePointsForCleaning(backUp);
                if (count < minCount)
                {
                    minCount = count;
                    minCleaning = limit.ToRestore();
                }
            }

            return minCleaning;
        }

        
        private void AddAvailableLimits()
        {
            if (CheckCount())
            {
                _availableLimits.Add(new ClearingByCount(ClearingCount));
            }

            if (CheckTime())
            {
                _availableLimits.Add(new ClearingByTime(ClearingTime));
            }

            if (CheckSize())
            {
                _availableLimits.Add(new ClearingBySize(ClearingSize));
            }
        }
    }
}