using System;
using System.Collections.Generic;
using System.IO;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class DeltaRestorePoint : RestorePoint
    {
        public override void CreateRestorePoint()
        {
            try
            {
                var previousRestorePoint = BackUp.Manager.RestorePoints.Peek();
                CreateRestoreForManyFile(FindModifiedFiles(previousRestorePoint));
                BackUp.Manager.RestorePoints.Enqueue(this);
                BackUp.BackUpComponents.Add(storage.Build());
            }
            catch
            {
                throw new RestorePointException();
            }
        }

        private List<string> FindModifiedFiles(RestorePoint previousRestorePoint)
        {
            List<string> modifiedList = new List<string>();
            foreach (var filePath in previousRestorePoint.EveryFileInfo.Keys)
            {
                if (FileRestoreCopyInfo(filePath).Size != 
                    previousRestorePoint.EveryFileInfo[filePath].Size)
                {
                    modifiedList.Add(filePath);
                }
            }

            return modifiedList;
        }
        
        private void CreateRestoreForManyFile(List<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                everyFileInfo.Add(filePath, CreateRestore(filePath));
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

        private static FileRestoreCopyInfo FileRestoreCopyInfo(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileRestoreCopyInfo = new FileRestoreCopyInfo
                (filePath, fileInfo.Length, DateTime.Now);
            return fileRestoreCopyInfo;
        }
        
        public DeltaRestorePoint(BackUp backUp) : base(backUp)
        {
            everyFileInfo = new Dictionary<string, FileRestoreCopyInfo>();
            createDateTime = DateTime.Now;
        }
        
        RestorePointStorageBuilder TypeRestorePointBackUpStorage => new RestorePointStorageBuilder(BackUp);
    }
}