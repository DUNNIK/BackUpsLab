using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.Storage.FolderClass
{
    public class Folder : IStorageComponent
    {
        protected internal readonly string FolderPath;
        protected List<IStorageComponent> FolderFiles = new List<IStorageComponent>();
        public Folder()
        {
            const string backUpsFolder = @"C:\Users\NIKITOS\RiderProjects\BackUpsLab\BackUpsLab\BackUpsFolder";
            var folder = @$"{Guid.NewGuid().ToString()}";
            FolderPath = Path.Combine(backUpsFolder, folder);
            Directory.CreateDirectory(FolderPath);
        }

        public Folder(string folderPath)
        {
            var backUpsFolder = folderPath;
            var folder = @$"{Guid.NewGuid().ToString()}";
            FolderPath = Path.Combine(backUpsFolder, folder);
            Directory.CreateDirectory(FolderPath);
        }

        public long Size()
        {
            return FolderFiles.Sum(file => file.Size());
        }

        public void AddFolderFile(IStorageComponent file)
        {
            FolderFiles.Add(file);
        }
    }
}