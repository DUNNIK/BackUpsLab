using System;
using System.Threading.Tasks;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public abstract class RestorePointClearing : BackUpBuilder
    {
        public DateTime ClearTime;
        public int ClearCount;
        public long ClearSize;

        protected RestorePointClearing(BackUp backUp) : base(backUp)
        {
        }
    }
}