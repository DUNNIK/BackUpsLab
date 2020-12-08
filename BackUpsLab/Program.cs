using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
                    .AddRestorePointType
                    .FullRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                // testBackUp
                //     .AddRestorePointClearing
                //     .ByCount(2);
                using (var fstream = File.OpenWrite(filePaths[1]))
                {
                    fstream.SetLength(12);
                }

                

                testBackUp
                    .AddRestorePointType
                    .DeltaRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                testBackUp
                    .AddRestorePointClearing
                    .BySize(100);
                
                // testBackUp
                //     .AddRestorePointType
                //     .FullRestorePoint()
                //     .AddRestorePointStorageType
                //     .Folder()
                //     .CreatePoint();
                testBackUp
                    .StopClearing();
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