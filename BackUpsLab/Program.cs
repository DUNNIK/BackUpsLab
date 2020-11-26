using System;
using System.Collections.Generic;
using System.IO;
using BackUpsLab.BackUp;
using BackUpsLab.Exceptions;
using static System.Console;

namespace BackUpsLab
{
    public static class Program
    {
        private static void Main()
        {
            try
            {
                List<string> filePaths = new List<string>
                {
                    @"C:\Users\NIKITOS\RiderProjects\BackUpsLab\BackUpsLab\Program.cs",
                    @"C:\Users\NIKITOS\RiderProjects\BackUpsLab\BackUpsLab\BackUp\BackUpBuilder.cs",
                    @"C:\Users\NIKITOS\RiderProjects\BackUpsLab\BackUpsLab\BackUp\Interfaces\IStorageCreator.cs"
                };

                BackUpBuilder test = new BackUpBuilder(filePaths);

                test
                    .AddBackUpStorageType
                    .Folder()
                    .CreatStorage()
                    .AddRestorePointType
                    .FullRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
            }
            catch (ArchiveCreationException e)
            {
                WriteLine(e.Message);
            }
            catch (FileAddException e)
            {
                WriteLine(e.Message);
            }
            catch (FileRemoveException e)
            {
                WriteLine(e.Message);
            }
            catch (RestorePointException e)
            {
                WriteLine(e.Message);
            }
        }
    }
}