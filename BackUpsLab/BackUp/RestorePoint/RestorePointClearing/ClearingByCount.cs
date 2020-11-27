using System.Threading.Tasks;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public static class ClearingByCount
    {
        public static async void ClearRestorePoint(BackUp backUp, int clearCount)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (IsCountTrue(backUp, clearCount))
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
        }

        public static bool IsCountTrue(BackUp backUp, int clearCount)
        {
            return backUp.Manager.RestorePoints.Count > clearCount;
        }
    }
}