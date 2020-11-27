using System;
using System.Collections.Generic;
using System.IO;
using BackUpsLab.BackUp;
using BackUpsLab.Exceptions;
using static System.Console;

namespace BackUpsLab
{
    public class Program
    {
        private static void Main()
        {
            try
            {
                var filePaths = new List<string>
                {
                    @"D:\Физика\измерения 3.13\Измерения1.txt",
                    @"D:\Физика\измерения 3.13\Измерения4.txt"
                };
                
                var testBackUp = new BackUpBuilder(filePaths);

                testBackUp
                    .AddBackUpStorageType
                    .Folder()
                    .CreatStorage()
                    .AddRestorePointClearing
                    .ByCount(2)
                    .AddRestorePointType
                    .FullRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                using (var fstream = File.OpenWrite(filePaths[1]))
                {
                    fstream.SetLength(10);
                }

                testBackUp
                    .AddRestorePointType
                    .DeltaRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                testBackUp
                    .AddRestorePointType
                    .FullRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                testBackUp
                    .AddRestorePointType
                    .DeltaRestorePoint()
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