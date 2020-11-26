using System;
using System.IO;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class FullRestorePoint : RestorePoint
    {
        public FullRestorePoint(BackUp backUp) : base(backUp)
        {
            
        }

        private void CreateRestoreForEveryFile()
        {
            var filePaths = BackUp.FilePaths;
            foreach (var filePath in filePaths)
            {
                EveryFileInfo.Add(filePath, CreateRestore(filePath));
            }
        }
        public override void CreateRestorePoint()
        {
            try
            {
                CreateRestoreForEveryFile();
                BackUp.Manager.RestorePoints.Enqueue(this);
            }
            catch
            {
                throw new RestorePointException();
            }
        }

        public override FileRestoreCopyInfo CreateRestore(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileRestoreCopyInfo = new FileRestoreCopyInfo
                (filePath, fileInfo.Length, DateTime.Now);
            Storage.AddFileTo(filePath);
            return fileRestoreCopyInfo;
        }
    }
}