using System;
using System.Threading.Tasks;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class ClearingByDate : Interfaces.RestorePointClearing
    {
        public static async void ClearRestorePoint(BackUp backUp, DateTime clearTime)
        {
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsTimeTrue(backUp, clearTime))
                    {
                        RemoveLastRestorePoint(backUp);
                    }
                }
            });
        }

        private static void RemoveLastRestorePoint(BackUp backUp)
        {
            var firstPoint = backUp.Manager.RestorePoints.Dequeue();
            firstPoint.Storage.RemoveAll();
            backUp.BackUpComponents.Remove(firstPoint.Storage.Build());
        }

        private static bool IsTimeTrue(BackUp backUp, DateTime clearTime)
        {
            return backUp.Manager.RestorePoints.Peek().CreateDateTime < clearTime;
        }
    }
}