using System.Linq;
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
                while (!Stop && IsBackUpUse(backUp))
                {
                    if (IsSizeTrue(backUp, ClearingSize) && backUp.Manager.RestorePoints.Any())
                    {
                        RemoveLastRestorePoint(backUp);
                    }
                }
            });
        }
        public Interfaces.RestorePointClearing ToRestore() => this;
        public int CountRestorePointsForCleaning(BackUp backUp)
        {
            var result = 0;
            var backUpSize = backUp.Size();
            foreach (var storage in backUp.BackUpComponents)
            {
                if (backUpSize > ClearingSize)
                {
                    result++;
                    backUpSize -= storage.Size();
                }
            }
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
            var backUpSize = backUp.Size();
            return backUp.Size() > clearSize;
        }
    }
}