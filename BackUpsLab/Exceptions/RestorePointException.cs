using System;

namespace BackUpsLab.Exceptions
{
    public class RestorePointException : Exception
    {
        public RestorePointException() : base("It is impossible to create restore point!")
        {
        }
    }
}