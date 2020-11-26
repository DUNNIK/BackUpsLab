using System;

namespace BackUpsLab.Exceptions
{
    public class FileRemoveException : Exception
    {
        public FileRemoveException() : base("Unable to remove file!")
        {
        }
    }
}