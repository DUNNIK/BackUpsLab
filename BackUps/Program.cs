using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackUps.BackUp;
using BackUps.BackUp.RestorePoint.RestorePointClearing;
using BackUps.Exceptions;
using BackUpsLab.BackUp;
using static System.Console;

namespace BackUps
{
    public static class Program
    {
#pragma warning disable 1998
        private static async Task Main()
#pragma warning restore 1998
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
                    fstream.SetLength(4);
                }
                

                testBackUp
                    .AddRestorePointType
                    .DeltaRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                // testBackUp
                //     .AddRestorePointClearing
                //     .ByTime(DateTime.Now);

                testBackUp
                    .AddRestorePointClearing
                    .AddComboParams
                    .AddLimitCount(3)
                    .AddLimitSize(100)
                    .AddLimitTime(DateTime.Now)
                    .Combo(ComboClearing.ComboType.Max);
                testBackUp
                    .AddRestorePointType
                    .FullRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                testBackUp
                    .AddRestorePointType
                    .FullRestorePoint()
                    .AddRestorePointStorageType
                    .Folder()
                    .CreatePoint();
                
                
                //testBackUp.StopClearing();
                testBackUp.WaitCleaning();
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