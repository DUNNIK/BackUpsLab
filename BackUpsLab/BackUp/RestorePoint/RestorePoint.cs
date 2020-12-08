using System;
using System.Collections.Generic;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.RestorePoint
{
    public abstract class RestorePoint : BackUpBuilder
    {
        protected IStorageCreator storage;

        protected DateTime createDateTime;
        
        protected Dictionary<string, FileRestoreCopyInfo> everyFileInfo;

        protected RestorePoint(BackUp backUp) : base(backUp)
        {
        }

        public abstract void CreateRestorePoint();
        public abstract FileRestoreCopyInfo CreateRestore(string filePath);

        public Dictionary<string, FileRestoreCopyInfo> EveryFileInfo => everyFileInfo;

        public IStorageCreator Storage => storage;

        public DateTime CreateDateTime => createDateTime;

        public void AddStorage(IStorageCreator storage)
        {
            this.storage = storage;
        }
    }
}