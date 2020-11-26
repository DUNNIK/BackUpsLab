using System.Collections.Generic;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.BackUp.RestorePoint;

namespace BackUpsLab.BackUp
{
    public class BackUp
    {
        protected internal IStorageCreator Storage;
        protected internal List<string> FilePaths = new List<string>();
        protected internal RestorePointManager Manager = new RestorePointManager();
        private long Size;
    }
}