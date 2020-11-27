using System;
using System.Threading.Tasks;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class RestorePointClearingBuilder : BackUpBuilder
    {
        public RestorePointClearingBuilder(BackUp backUp) : base(backUp)
        {
        }
        public RestorePointClearingBuilder ByCount(int count)
        {
            ClearingByCount.ClearRestorePoint(BackUp, count);
            return this;
        }

        public RestorePointClearingBuilder ByDate()
        {
            return this;
        }

        public RestorePointClearingBuilder BySize()
        {
            return this;
        }

        public RestorePointClearingBuilder Combo()
        {
            return this;
        }

        public RestorePointClearingBuilder AddCount(int maxCount)
        {
            BackUp.ClearingType.ClearCount = maxCount;
            return this;
        }

        public RestorePointClearingBuilder AddDate(DateTime maxTime)
        {
            BackUp.ClearingType.ClearTime = maxTime;
            return this;
        }

        public RestorePointClearingBuilder AddSize(long maxSize)
        {
            BackUp.ClearingType.ClearSize = maxSize;
            return this;
        }
    }
}