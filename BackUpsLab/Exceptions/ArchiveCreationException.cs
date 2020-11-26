using System;

namespace BackUpsLab.Exceptions
{
    public class ArchiveCreationException : Exception
    {
        public ArchiveCreationException() : base("It is impossible to create an archive!")
        {
        }
    }
}