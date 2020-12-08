using System.Collections.Generic;
using System.Linq;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.BackUp.RestorePoint;
using BackUpsLab.BackUp.RestorePoint.RestorePointClearing;

namespace BackUpsLab.BackUp
{
    public class BackUp : IStorageComponent
    {
        protected internal IStorageCreator Storage;
        protected internal List<string> FilePaths = new List<string>();
        protected internal RestorePointManager Manager = new RestorePointManager();
        protected internal List<IStorageComponent> BackUpComponents = new List<IStorageComponent>();
        public long Size()
        {
            return BackUpComponents.Sum(file => file.Size());
        }
    }
}