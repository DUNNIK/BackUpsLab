using System.IO;
using BackUpsLab.BackUp.Interfaces;

namespace BackUpsLab.BackUp.Storage
{
    public class OrdinaryFile : IStorageComponent
    {
        private string filePath;

        public OrdinaryFile(string filePath)
        {
            this.filePath = filePath;
        }

        public long Size()
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }
        
        public string FilePath => filePath;
    }
}