using System;
using System.Threading.Tasks;

namespace BackUpsLab.BackUp.Interfaces
{
    public abstract class RestorePointClearing
    {
        public static bool Stop = false;
        
        protected int ClearingCount = int.MaxValue;
        protected DateTime ClearingTime = DateTime.MaxValue;
        protected long ClearingSize = long.MaxValue;

        public abstract void ClearRestorePoint(BackUp backUp);
        protected bool IsBackUpUse(BackUp backUp)
        {
            if ((DateTime.Now - backUp.LastUse).Seconds > 60)
            {
                return false;
            }

            return true;
        }
    }

    public interface IRestorePointCount
    {
        public int CountRestorePointsForCleaning(BackUp backUp);
        public RestorePointClearing ToRestore();
    }
}