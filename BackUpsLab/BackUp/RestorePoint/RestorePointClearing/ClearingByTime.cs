using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: Enumerator[BackUpsLab.BackUp.RestorePoint.RestorePoint]")]
        public override async void ClearRestorePoint(BackUp backUp)
        {
            await Task.Run(() =>
            {
                while (!Stop && IsBackUpUse(backUp))
                {
                    if (IsTimeTrue(backUp, ClearingTime) & backUp.Manager.RestorePoints.Any())
                    {
                        RemoveLastRestorePoint(backUp);
                    }
                }
            });
        }

        public int CountRestorePointsForCleaning(BackUp backUp)
        {
            return backUp.Manager.RestorePoints.Count(point => point.CreateDateTime < ClearingTime);
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