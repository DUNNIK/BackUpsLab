using System.Threading.Tasks;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class ClearingBySize : Interfaces.RestorePointClearing
    {
        public static async void ClearRestorePoint(BackUp backUp, long clearSize)
        {
            await Task.Run(() =>
            {
                while (!Stop)
                {
                    if (IsSizeTrue(backUp, clearSize))
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

        private static bool IsSizeTrue(BackUp backUp, long clearSize)
        {
            return backUp.Size() > clearSize;
        }
    }
}