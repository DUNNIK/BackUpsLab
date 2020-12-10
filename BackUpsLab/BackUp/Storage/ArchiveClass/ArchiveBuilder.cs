using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.Storage.ArchiveClass
{
    
    internal class ArchiveBuilder : IStorageCreator
    {
        private readonly Archive _archive;

        public ArchiveBuilder() => _archive = new Archive();
        public ArchiveBuilder(string path)
        {
            _archive = new Archive(path);
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
            Thread.Sleep(4000);
            using (ZipArchive zipArchive = ZipFile.Open(_archive.ArchivePath, 
                ZipArchiveMode.Update))
            {
                zipArchive.CreateEntryFromFile(filePath, 
                    System.IO.Path.GetFileName(filePath));
            }
        }

        private void DeleteOneFile(string filePath)
        {
            using (ZipArchive zipArchive = ZipFile.Open(_archive.ArchivePath, 
                ZipArchiveMode.Update))
            {
                zipArchive
                    .Entries.FirstOrDefault
                    (x => 
                        x.Name == System.IO.Path.GetFileName(filePath))?.Delete();
            }
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
                DeleteOneFile(filePath);
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
                File.Delete(_archive.ArchivePath);
            }
            catch
            {
                throw new FileRemoveException();
            }
        }

        public string Path()
        {
            return _archive.ArchivePath;
        }

        public IStorageComponent Build() => _archive;
    }
}