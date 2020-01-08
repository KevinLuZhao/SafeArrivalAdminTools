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

        private async void DeliverApplications()
        {

        }
    }
}
