using System.Collections.Generic;
using BackUpsLab.BackUp.RestorePoint;
using BackUpsLab.BackUp.Storage;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp
{
    public class BackUpBuilder
    {
        protected readonly BackUp BackUp = new BackUp();
        public BackUpBuilder(List<string> filePaths)
        {
            BackUp.FilePaths = filePaths;
        }

        public BackUpBuilder CreatePoint()
        {
            BackUp.Manager.NewPoint.CreateRestorePoint();
            return this;
        }
        public BackUpBuilder CreatStorage()
        {
            BackUp.Storage.Create(BackUp.FilePaths);
            return this;
        }
        protected BackUpBuilder(BackUp backUp) => BackUp = backUp;

        public BackUpBuilder AddFile(string filePath)
        {
            if (BackUp.FilePaths.Contains(filePath))
            {
                throw new FileAddException();
            }
            BackUp.FilePaths.Add(filePath);
            BackUp.Storage.AddFileTo(filePath);
            return this;
        }

        public BackUpBuilder RemoveFile(string filePath)
        {
            if (!BackUp.FilePaths.Contains(filePath))
            {
                throw new FileRemoveException();
            }
            BackUp.FilePaths.Remove(filePath);
            BackUp.Storage.RemoveFileFrom(filePath);
            return this;
        }
        
        public BackUpStorageBuilder AddBackUpStorageType => new BackUpStorageBuilder(BackUp);
        
        public RestorePointBuilder AddRestorePointType => new RestorePointBuilder(BackUp);
        
        public RestorePointStorageBuilder AddRestorePointStorageType => new RestorePointStorageBuilder(BackUp);
        
        public override string ToString() => BackUp.ToString();
        
        public BackUp Build() => BackUp;
        
        public static implicit operator BackUp(BackUpBuilder builder) 
            => builder.BackUp;
        
    }
}