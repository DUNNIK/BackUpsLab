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
                var previousRestorePoint 
                    = BackUp.Manager.RestorePoints.Peek();
                CreateRestoreForManyFile(FindModifiedFiles(previousRestorePoint));
                BackUp.Manager.RestorePoints.Enqueue(this);
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
                if (EveryFileInfo[filePath].Size != previousRestorePoint.EveryFileInfo[filePath].Size)
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
                EveryFileInfo.Add(filePath, CreateRestore(filePath));
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

        public DeltaRestorePoint(BackUp backUp) : base(backUp)
        {
            EveryFileInfo = new Dictionary<string, FileRestoreCopyInfo>();
        }
        
        RestorePointStorageBuilder TypeRestorePointBackUpStorage => new RestorePointStorageBuilder(BackUp);
    }
}