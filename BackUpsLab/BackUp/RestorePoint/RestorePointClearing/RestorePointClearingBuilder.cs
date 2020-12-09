using System;

namespace BackUpsLab.BackUp.RestorePoint.RestorePointClearing
{
    public class RestorePointClearingBuilder : BackUpBuilder
    {
        private Interfaces.RestorePointClearing _cleaning;
        protected int ClearingCount = int.MaxValue;
        protected DateTime ClearingTime = DateTime.MaxValue;
        protected long ClearingSize = long.MaxValue;
        public RestorePointClearingBuilder(BackUp backUp) : base(backUp)
        {
        }
        public RestorePointClearingBuilder ByCount(int count)
        {
            _cleaning = new ClearingByCount(count);
            _cleaning.ClearRestorePoint(BackUp);
            return this;
        }

        public RestorePointClearingBuilder ByTime(DateTime time)
        {
            _cleaning = new ClearingByTime(time);
            _cleaning.ClearRestorePoint(BackUp);
            return this;
        }

        public RestorePointClearingBuilder BySize(long size)
        {
            _cleaning = new ClearingBySize(size);
            _cleaning.ClearRestorePoint(BackUp);
            return this;
        }

        public RestorePointClearingBuilder Combo(ComboClearing.ComboType type)
        {
            _cleaning = new ComboClearing(type, ClearingTime, ClearingCount, ClearingSize);
            return this;
        }
        
        public ComboBuilder AddComboParams => new ComboBuilder(BackUp);
    }
    
    public class ComboBuilder : RestorePointClearingBuilder
    {
        public ComboBuilder(BackUp backUp) : base(backUp)
        {
        }
        public ComboBuilder AddLimitCount(int count)
        {
            ClearingCount = count;
            return this;
        }

        public ComboBuilder AddLimitSize(long size)
        {
            ClearingSize = size;
            return this;
        }

        public ComboBuilder AddLimitTime(DateTime time)
        {
            ClearingTime = time;
            return this;
        }
    }
}