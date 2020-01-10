using LibGit2Sharp;
using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.BLL
{
    public class SoftwareDeliveryService
    {
        string tempPath = @"c:\temp\";

        public async Task ExportParameters()
        {
            var paramsFilePath = $"{tempPath}parameter.zip";
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
                return;
            }
            if (File.Exists(paramsFilePath))
            {
                File.Delete(paramsFilePath);
            }

            using (FileStream zipToOpen = new FileStream(paramsFilePath, FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry;
                    DirectoryInfo d = new DirectoryInfo(ConfigurationManager.AppSettings["ParammeterFilesFolder"] + GlobalVariables.Enviroment);
                    FileInfo[] Files = d.GetFiles("*.json");
                    foreach (FileInfo file in Files)
                    {
                        readmeEntry = archive.CreateEntryFromFile(file.FullName, file.Name);
                    }
                }
                zipToOpen.Close();
            }
            await UploadParameterZipToS3();
        }

        private async Task UploadParameterZipToS3()
        {
            S3Helper s3Helper = new S3Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color,
                string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "parameters")
                );

            await s3Helper.UploadFile("parameter.zip", new FileStream($"{tempPath}parameter.zip", FileMode.Open));
        }

        public async Task ExportCloudFormation()
        {
            var zipFilePath = GenerateInfraZip();
            S3Helper s3Helper = new S3Helper(
             GlobalVariables.Enviroment,
             GlobalVariables.Region,
             GlobalVariables.Color,
             string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "cloudformation")
             );

            await s3Helper.UploadFile(Path.GetFileName(zipFilePath), new FileStream(zipFilePath, FileMode.Open));
        }

        private string GenerateInfraZip()
        {
            var zipFilePath = $"{tempPath}safearrival-infra.zip";
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
                return null;
            }
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry;
                    var dir = new DirectoryInfo(ConfigurationManager.AppSettings["InfraFileFolder"]);
                    foreach (var subFolder in dir.GetDirectories())
                    {
                        if (subFolder.Name == ".git")
                            continue;
                        // = archive.CreateEntry(subFolder.Name);
                        foreach (var file in subFolder.GetFiles())
                        {
                            readmeEntry = archive.CreateEntryFromFile(file.FullName, subFolder.Name + "/" + file.Name);
                        }
                    }
                }
                zipToOpen.Close();
            }
            return zipFilePath;
        }

        public async Task DeliverApplications(List<string> applicationExportingLogs)
        {
            //await CopyApplicationZipFiles(applicationExportingLogs);
            await UpdateLambdaFunctionVersion();
            await Task.Run(() =>
            {

                //for (var i = 0; i < 100; i++)
                //{
                //    Thread.Sleep(1000);
                //    applicationExportingLogs.Add($"This is {i}");
                //}
            });
        }

        private async Task CopyApplicationZipFiles(List<string> applicationExportingLogs)
        {
            //var sourceFolder = "application_sources/";
            var sourceFolder = "application/";
            var detinationFolder = "application/";
            S3Helper s3Helper = new S3Helper(
               GlobalVariables.Enviroment,
               GlobalVariables.Region,
               GlobalVariables.Color,
               string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "artifact")
            );

            var sourceFiles = s3Helper.GetFolderFileKeys(s3Helper.BucketName, sourceFolder);
            foreach (var sourceKey in sourceFiles)
            {
                if (sourceKey.Trim() == sourceFolder)
                    continue;
                var destinationKey = sourceKey.Replace(sourceFolder, detinationFolder);
                //await s3Helper.CopyFile(s3Helper.BucketName, sourceKey, s3Helper.BucketName, destinationKey);
                applicationExportingLogs.Add($"{DateTime.Now}:  {sourceKey} copied to {destinationKey}");
            }
            //await s3Helper.CopyFolder(s3Helper.BucketName, "application/", s3Helper.BucketName, "application_sources/");
        }

        private async Task UpdateLambdaFunctionVersion()
        {
            LambdaHelper helper = new LambdaHelper(
             GlobalVariables.Enviroment,
             GlobalVariables.Region,
             GlobalVariables.Color);
            helper.ReadTag("SafeArrival-Lambda-SisFetcher-staging-green", "Version");
        }
    }
}
