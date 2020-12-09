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
    }

    public interface IRestorePointCount
    {
        public Task<int> CountRestorePointsForCleaning(BackUp backUp);
        public RestorePointClearing ToRestore();
    }
}