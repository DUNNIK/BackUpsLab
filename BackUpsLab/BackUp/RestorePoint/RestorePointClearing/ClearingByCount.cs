﻿using System;
using System.Threading.Tasks;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class ClearingByCount : Interfaces.RestorePointClearing, IRestorePointCount
    {
        public ClearingByCount(int count)
        {
            ClearingCount = count;
        }
        public override async void ClearRestorePoint(BackUp backUp)
        {
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsCountTrue(backUp, ClearingCount))
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
                    if (IsCountTrue(backUp, ClearingCount))
                    {
                        result++;
                    }
                }
            });
            return result;
        }

        public Interfaces.RestorePointClearing ToRestore() => this;

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

        private static bool IsCountTrue(BackUp backUp, int clearCount)
        {
            return backUp.Manager.RestorePoints.Count > clearCount;
        }
    }
}