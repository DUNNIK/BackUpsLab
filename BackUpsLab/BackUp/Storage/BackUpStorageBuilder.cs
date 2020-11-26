using BackUpsLab.BackUp.Storage.ArchiveClass;
using BackUpsLab.BackUp.Storage.FolderClass;

namespace BackUpsLab.BackUp.Storage
{
    public class BackUpStorageBuilder : BackUpBuilder
    {
        public BackUpStorageBuilder(BackUp backUp) : base(backUp)
        {
        }

        public BackUpStorageBuilder Archive()
        {
            BackUp.Storage = new ArchiveBuilder();
            return this;
        }

        public BackUpStorageBuilder Folder()
        {
            BackUp.Storage = new FolderBuilder();
            return this;
        }
    }

}