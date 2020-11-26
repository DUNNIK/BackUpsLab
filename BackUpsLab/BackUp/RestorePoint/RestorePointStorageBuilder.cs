using BackUpsLab.BackUp.Storage.ArchiveClass;
using BackUpsLab.BackUp.Storage.FolderClass;

namespace BackUpsLab.BackUp.RestorePoint
{
    public class RestorePointStorageBuilder : BackUpBuilder
    {
        public RestorePointStorageBuilder Archive()
        {
            BackUp.Manager.NewPoint.Storage = new ArchiveBuilder(@$"{BackUp.Storage.Path()}");
            return this;
        }

        public RestorePointStorageBuilder Folder()
        {
            BackUp.Manager.NewPoint.Storage = new FolderBuilder(@$"{BackUp.Storage.Path()}");
            return this;
        }

        public RestorePointStorageBuilder(BackUp backUp) : base(backUp)
        {
        }
    }
}