namespace BackUpsLab.BackUp.RestorePoint
{
    public class RestorePointBuilder : BackUpBuilder
    {
        public RestorePointBuilder(BackUp backUp) : base(backUp)
        {
        }

        public RestorePointBuilder FullRestorePoint()
        {
            BackUp.Manager.NewPoint = new FullRestorePoint(BackUp);
            return this;
        }

        public RestorePointBuilder DeltaRestorePoint()
        {
            BackUp.Manager.NewPoint = new DeltaRestorePoint(BackUp);
            return this;
        }
    }
}