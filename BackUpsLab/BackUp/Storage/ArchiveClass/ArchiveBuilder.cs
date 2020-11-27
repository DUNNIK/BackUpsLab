using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.Storage.ArchiveClass
{
    internal class ArchiveBuilder : IStorageCreator
    {
        private Archive Archive;

        public ArchiveBuilder() => Archive = new Archive();
        public ArchiveBuilder(string path)
        {
            Archive = new Archive(path);
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
            using (ZipArchive zipArchive = ZipFile.Open(Archive.ArchivePath, 
                ZipArchiveMode.Update))
            {
                zipArchive.CreateEntryFromFile(filePath, 
                    System.IO.Path.GetFileName(filePath));
            }
        }

        private void DeleteOneFile(string filePath)
        {
            using (ZipArchive zipArchive = ZipFile.Open(Archive.ArchivePath, 
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
            File.Delete(Archive.ArchivePath);
        }

        public string Path()
        {
            return Archive.ArchivePath;
        }
    }
}