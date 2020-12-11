using System;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class FileRestoreCopyInfo
    {
        private string _filePath;
        public readonly long Size;
        private DateTime _time;

        public FileRestoreCopyInfo(string filePath, long size, DateTime time)
        {
            _filePath = filePath;
            Size = size;
            _time = time;
        }
    }
}