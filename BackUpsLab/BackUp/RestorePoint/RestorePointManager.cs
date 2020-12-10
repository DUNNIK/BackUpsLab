using System.Collections.Generic;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class RestorePointManager
    {
        public readonly Queue<RestorePoint> RestorePoints 
            = new Queue<RestorePoint>();

        public RestorePoint NewPoint;
    }
}