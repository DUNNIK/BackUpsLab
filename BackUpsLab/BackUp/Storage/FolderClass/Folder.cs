using System;
using System.IO;

namespace BackUpsLab.BackUp.Storage.FolderClass
{
    public class Folder
    {
        protected internal readonly string FolderPath;
        public Folder()
        {
            const string backUpsFolder = @"C:\Users\NIKITOS\RiderProjects\BackUpsLab\BackUpsLab\BackUpsFolder";
            var folder = @$"{Guid.NewGuid().ToString()}";
            FolderPath = Path.Combine(backUpsFolder, folder);
            Directory.CreateDirectory(FolderPath);
        }

        public Folder(string folderPath)
        {
            string backUpsFolder = folderPath;
            var folder = @$"{Guid.NewGuid().ToString()}";
            FolderPath = Path.Combine(backUpsFolder, folder);
            Directory.CreateDirectory(FolderPath);
        }
    }
}