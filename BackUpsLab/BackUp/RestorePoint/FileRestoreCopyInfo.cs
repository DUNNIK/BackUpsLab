using System;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class FileRestoreCopyInfo
    {
        public string FilePath;
        public long Size;
        public DateTime Time;

        public FileRestoreCopyInfo(string filePath, long size, DateTime time)
        {
            FilePath = filePath;
            Size = size;
            Time = time;
        }
    }
}