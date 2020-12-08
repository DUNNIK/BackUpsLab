using System;
using System.Collections.Generic;
using System.IO;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class FullRestorePoint : RestorePoint
    {
        public FullRestorePoint(BackUp backUp) : base(backUp)
        {
            everyFileInfo = new Dictionary<string, FileRestoreCopyInfo>();
            createDateTime = DateTime.Now;
        }

        private void CreateRestoreForEveryFile()
        {
            var filePaths = BackUp.FilePaths;
            foreach (var filePath in filePaths)
            {
                everyFileInfo.Add(filePath, CreateRestore(filePath));
            }
        }
        public override void CreateRestorePoint()
        {
            try
            {
                CreateRestoreForEveryFile();
                BackUp.Manager.RestorePoints.Enqueue(this);
                BackUp.BackUpComponents.Add(storage.Build());
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
            storage.AddFileTo(filePath);
            return fileRestoreCopyInfo;
        }
    }
}