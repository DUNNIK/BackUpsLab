using System.Linq;
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
                while (!Stop && IsBackUpUse(backUp))
                {
                    if (IsCountTrue(backUp, ClearingCount) && backUp.Manager.RestorePoints.Any())
                    {
                        RemoveLastRestorePoint(backUp);
                    }
                }
            });
        }

        public int CountRestorePointsForCleaning(BackUp backUp)
        {
            return backUp.Manager.RestorePoints.Count - ClearingCount;
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