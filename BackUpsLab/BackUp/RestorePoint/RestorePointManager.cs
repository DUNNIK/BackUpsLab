using System.Collections.Generic;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class RestorePointManager
    {
        public Queue<RestorePoint> RestorePoints 
            = new Queue<RestorePoint>();

        public RestorePoint NewPoint;
    }
}