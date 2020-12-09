using System.Threading.Tasks;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class ClearingBySize : Interfaces.RestorePointClearing, IRestorePointCount
    {
        public ClearingBySize(long size)
        {
            ClearingSize = size;
        }
        public override async void ClearRestorePoint(BackUp backUp)
        {
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsSizeTrue(backUp, ClearingSize))
                    {
                        RemoveLastRestorePoint(backUp);
                    }
                }
            });
        }

        public Interfaces.RestorePointClearing ToRestore() => this;
        public async Task<int> CountRestorePointsForCleaning(BackUp backUp)
        {
            var result = 0;
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsSizeTrue(backUp, ClearingSize))
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
                backUp.BackUpComponents.Remove(firstPoint.Storage.Build());
                firstPoint.Storage.RemoveAll();
            }
            catch
            {
                throw new FileRemoveException();
            }
        }

        private static bool IsSizeTrue(IStorageComponent backUp, long clearSize)
        {
            return backUp.Size() > clearSize;
        }
    }
}