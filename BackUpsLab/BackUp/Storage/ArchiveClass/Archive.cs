using System;
using System.IO;
using System.IO.Compression;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.Exceptions;

namespace BackUpsLab.BackUp.Storage.ArchiveClass
{
    public class Archive : IStorageComponent
    {
        protected internal readonly string ArchivePath;
        public Archive()
        {
            const string folder = @"C:\Users\NIKITOS\RiderProjects\BackUpsLab\BackUpsLab\BackUpsFolder";
            var file = @$"{Guid.NewGuid().ToString()}.zip";
            ArchivePath = Path.Combine(folder, file);
            CreateNewArchive(ArchivePath);
        }
        
        public Archive(string folderPath)
        {
            var folder = folderPath;
            var file = @$"{Guid.NewGuid().ToString()}.zip";
            ArchivePath = Path.Combine(folder, file);
            CreateNewArchive(ArchivePath);
        }

        private static void CreateNewArchive(string archivePath)
        {
            try
            {
                ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
            }
            catch
            {
                throw new ArchiveCreationException();
            }
        }

        public long Size()
        {
            long resultSize = 0;
            using (ZipArchive archive = ZipFile.OpenRead(ArchivePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    resultSize += entry.Length;
                }
            }
            return resultSize;
        }
    }
}