using System;
using System.Threading.Tasks;
using static BackUpsLab.BackUp.RestorePoint.RestorePointClearing.ClearingByCount;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class RestorePointClearingBuilder : BackUpBuilder
    {
        public RestorePointClearingBuilder(BackUp backUp) : base(backUp)
        {
        }
        public RestorePointClearingBuilder ByCount(int count)
        {
            ClearRestorePoint(BackUp, count);
            return this;
        }

        public RestorePointClearingBuilder ByDate(DateTime time)
        {
            ClearingByDate.ClearRestorePoint(BackUp, time);
            return this;
        }

        public RestorePointClearingBuilder BySize(long size)
        {
            ClearingBySize.ClearRestorePoint(BackUp, size);
            return this;
        }

        public RestorePointClearingBuilder Combo()
        {
            return this;
        }
    }
}