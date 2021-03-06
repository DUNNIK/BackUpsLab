﻿using System.IO;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.Storage
{
    public class OrdinaryFile : IStorageComponent
    {
        private readonly string _filePath;

        public OrdinaryFile(string filePath)
        {
            this._filePath = filePath;
        }

        public long Size()
        {
            var fileInfo = new FileInfo(_filePath);
            return fileInfo.Length;
        }
        
        public string FilePath => _filePath;
    }
}