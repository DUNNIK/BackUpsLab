using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackUpsLab.BackUp;
using BackUpsLab.BackUp.RestorePoint;
using BackUpsLab.BackUp.RestorePoint.RestorePointClearing;
using NUnit.Framework;

namespace BackUp.Tests
{
    [TestFixture]
    public class BackUpTest
    {
        [Test]
        public void BackUp_AddTwoRestorePointsAndStartCountCleaning_OneRestorePoint()
        {
            var filePaths = new List<string>
            {
                @"D:\Физика\измерения 3.13\Измерения1.txt",
                @"D:\Физика\измерения 3.13\Измерения4.txt"
            };
                
            var testBackUpBuilder = new BackUpBuilder(filePaths);
            testBackUpBuilder
                .AddBackUpStorageType
                .Folder()
                .CreatStorage()
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();
            
            Assert.That(testBackUpBuilder.Build().Manager.RestorePoints.Peek(), Is.InstanceOf(typeof(RestorePoint)));
            
            testBackUpBuilder
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();
            
            testBackUpBuilder
                .AddRestorePointClearing
                .ByCount(1);
            testBackUpBuilder.WaitCleaning();
            
            Assert.That(testBackUpBuilder.Build().Manager.RestorePoints.Count, Is.EqualTo(1));
        }

        [Test]
        public void BackUp_StartSizeCleaningForSpecificFiles_OneRestorePoint()
        {
            var filePaths = new List<string>
            {
                @"D:\Физика\измерения 3.13\Измерения1.txt",
                @"D:\Физика\измерения 3.13\Измерения4.txt"
            };
               
            using (var fstream1 = File.OpenWrite(filePaths[0]))
            {
                fstream1.SetLength(50);
            }
            using (var fstream2 = File.OpenWrite(filePaths[1]))
            {
                fstream2.SetLength(50);
            }
            
            var testBackUpBuilder = new BackUpBuilder(filePaths);

            testBackUpBuilder
                .AddBackUpStorageType
                .Folder()
                .CreatStorage()
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint()
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();
            
            Assert.That(testBackUpBuilder.Build().Manager.RestorePoints.Count, Is.EqualTo(2));
            Assert.That(testBackUpBuilder.Build().Size(), Is.EqualTo(200));

            testBackUpBuilder
                .AddRestorePointClearing
                .BySize(150);
            
            testBackUpBuilder.WaitCleaning();
            
            Assert.That(testBackUpBuilder.Build().Manager.RestorePoints.Count, Is.EqualTo(1));
        }

        [Test]
        public void BackUp_CreatDeltaRestore_OneFile()
        {
            var filePaths = new List<string>
            {
                @"D:\Физика\измерения 3.13\Измерения1.txt",
                @"D:\Физика\измерения 3.13\Измерения4.txt"
            };
               
            using (var fstream1 = File.OpenWrite(filePaths[0]))
            {
                fstream1.SetLength(50);
            }
            using (var fstream2 = File.OpenWrite(filePaths[1]))
            {
                fstream2.SetLength(50);
            }
            
            var testBackUpBuilder = new BackUpBuilder(filePaths);

            testBackUpBuilder
                .AddBackUpStorageType
                .Folder()
                .CreatStorage()
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();
            
            Assert.That(testBackUpBuilder.Build().Manager.RestorePoints.Count, Is.EqualTo(1));
            
            using (var fstream2 = File.OpenWrite(filePaths[1]))
            {
                fstream2.SetLength(10);
            }
            
            testBackUpBuilder
                .AddRestorePointType
                .DeltaRestorePoint()
                .AddRestorePointStorageType
                .Archive()
                .CreatePoint();
            
            Assert.That(testBackUpBuilder.Build().BackUpComponents.Last().Size(), Is.EqualTo(10));
        }
        
        [Test]
        public void BackUp_ChangeAlgorithms_EqualSizes()
        {
            var filePaths = new List<string>
            {
                @"D:\Физика\измерения 3.13\Измерения1.txt",
                @"D:\Физика\измерения 3.13\Измерения4.txt"
            };
            
            var testBackUpBuilder = new BackUpBuilder(filePaths);
            
            testBackUpBuilder
                .AddBackUpStorageType
                .Folder()
                .CreatStorage()
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();

            var lastByFolder = testBackUpBuilder.Build().BackUpComponents.Last();
            
            testBackUpBuilder
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Archive()
                .CreatePoint();
            var lastByArchive = testBackUpBuilder.Build().BackUpComponents.Last();
            
            Assert.That(lastByFolder.Size(), Is.EqualTo(lastByArchive.Size()));
        }

        [Test]
        public void BackUp_CreatComboRestore_TwoRestorePoints()
        {
            var filePaths = new List<string>
            {
                @"D:\Физика\измерения 3.13\Измерения1.txt",
                @"D:\Физика\измерения 3.13\Измерения4.txt"
            };
            using (var fstream1 = File.OpenWrite(filePaths[0]))
            {
                fstream1.SetLength(50);
            }

            using (var fstream2 = File.OpenWrite(filePaths[1]))
            {
                fstream2.SetLength(50);
            }

            var testBackUpBuilder = new BackUpBuilder(filePaths);

            testBackUpBuilder
                .AddBackUpStorageType
                .Folder()
                .CreatStorage()
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();

            using (var fstream = File.OpenWrite(filePaths[1]))
            {
                fstream.SetLength(2);
            }


            testBackUpBuilder
                .AddRestorePointType
                .DeltaRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();

            testBackUpBuilder
                .AddRestorePointClearing
                .AddComboParams
                .AddLimitCount(3)
                .AddLimitSize(100)
                .AddLimitTime(DateTime.Now)
                .Combo(ComboClearing.ComboType.Max);
            testBackUpBuilder
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();

            testBackUpBuilder
                .AddRestorePointType
                .FullRestorePoint()
                .AddRestorePointStorageType
                .Folder()
                .CreatePoint();


            testBackUpBuilder.WaitCleaning();

            Assert.That(testBackUpBuilder.Build().Manager.RestorePoints.Count, Is.EqualTo(2));
        }
    }
}