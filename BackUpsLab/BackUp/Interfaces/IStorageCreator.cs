using System.Collections.Generic;

namespace BackUpsLab.BackUp.Interfaces
{
    public interface IStorageCreator
    {
        void Create(List<string> files);
        void AddFileTo(string filePath);
        void RemoveFileFrom(string filePath);
        string Path();
    }
}