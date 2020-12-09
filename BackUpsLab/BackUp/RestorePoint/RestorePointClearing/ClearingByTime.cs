using System;
using System.Threading.Tasks;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class ClearingByTime : Interfaces.RestorePointClearing, IRestorePointCount
    {
        public ClearingByTime(DateTime time)
        {
            ClearingTime = time;
        }
        public override async void ClearRestorePoint(BackUp backUp)
        {
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsTimeTrue(backUp, ClearingTime))
                    {
                        RemoveLastRestorePoint(backUp);
                    }
                }
            });
        }

        public async Task<int> CountRestorePointsForCleaning(BackUp backUp)
        {
            var result = 0;
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsTimeTrue(backUp, ClearingTime))
                    {
                        result++;
                    }
                }
            });
            return result;
        }
        private static void RemoveLastRestorePoint(BackUp backUp)
        {
            try
            {
                var firstPoint = backUp.Manager.RestorePoints.Dequeue();
                firstPoint.Storage.RemoveAll();
                backUp.BackUpComponents.Remove(firstPoint.Storage.Build());
            }
            catch
            {
                throw new FileRemoveException();
            }
        }

        public Interfaces.RestorePointClearing ToRestore() => this;
        private static bool IsTimeTrue(BackUp backUp, DateTime clearTime)
        {
            return backUp.Manager.RestorePoints.Peek().CreateDateTime < clearTime;
        }
    }
}