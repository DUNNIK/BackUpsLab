using System.Collections.Generic;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.BackUp.Storage.ArchiveClass;
using BackUpsLab.BackUp.Storage.FolderClass;

namespace BackUpsLab.BackUp.RestorePoint
{
    public abstract class RestorePoint : BackUpBuilder
    {
        public IStorageCreator Storage;

        public Dictionary<string, FileRestoreCopyInfo> EveryFileInfo;

        protected RestorePoint(BackUp backUp) : base(backUp)
        {
        }

        public abstract void CreateRestorePoint();
        public abstract FileRestoreCopyInfo CreateRestore(string filePath); 
        
    }
}