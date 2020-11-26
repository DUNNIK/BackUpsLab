using System;

namespace BackUpsLab.Exceptions
{
    public class FileAddException : Exception
    {
        public FileAddException() : base("Unable to add file!")
        {
        }
    }
}