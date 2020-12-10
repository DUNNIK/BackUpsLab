using System;
using System.Collections.Generic;
using System.Linq;
using BackUpsLab.BackUp.Interfaces;
using BackUpsLab.BackUp.RestorePoint;
using BackUpsLab.BackUp.RestorePoint.RestorePointClearing;

namespace BackUpsLab.BackUp
{
    public class BackUp : IStorageComponent
    {
        public DateTime LastUse = DateTime.Now;
        protected internal IStorageCreator Storage;
        protected internal List<string> FilePaths = new List<string>();
        public readonly RestorePointManager Manager = new RestorePointManager();
        private List<IStorageComponent> _backUpComponents = new List<IStorageComponent>();
        public long Size()
        {
            return _backUpComponents.Sum(file => file.Size());
        }
        public List<IStorageComponent> BackUpComponents
        {
            get => _backUpComponents;
            set
            {
                _backUpComponents = value;
                LastUse = DateTime.Now;
            }
        }
    }
}