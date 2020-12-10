using System;
using System.Collections.Generic;
using System.IO;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.Storage.FolderClass
{
    internal class FolderBuilder : IStorageCreator
    {
        private Folder Folder;

        public FolderBuilder() => Folder = new Folder();
        public FolderBuilder(string path)
        {
            Folder = new Folder(path);
        }

        public void Create(List<string> files)
        {
            try
            {
                CreateFiles(files);
            }
            catch
            {
                throw new FileAddException();
            }
        }

        private void CreateFiles(List<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                CreateOneFile(filePath);
            }
        }

        private void CreateOneFile(string filePath)
        {
            File.Copy(filePath, 
                System.IO.Path
                    .Combine(Folder.FolderPath, System.IO.Path.GetFileName(filePath)), true);
            var file = new OrdinaryFile(filePath);
            Folder.AddFolderFile(file);
        }
        public void AddFileTo(string filePath)
        {
            try
            {
                CreateOneFile(filePath);
            }
            catch
            {
                throw new FileAddException();
            }
        }

        public void RemoveFileFrom(string filePath)
        {
            try
            {
                File.Delete(System.IO.Path.
                    Combine(Folder.FolderPath, System.IO.Path.GetFileName(filePath)));
            }
            catch
            {
                throw new FileRemoveException();
            }
        }

        public void RemoveAll()
        {
            try
            {
                Directory.Delete(Folder.FolderPath, true);
            }
            catch
            {
                throw new FileRemoveException();
            }

        }
        public string Path()
        {
            return Folder.FolderPath;
        }

        public IStorageComponent Build() => Folder;
    }
}