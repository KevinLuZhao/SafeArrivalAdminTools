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
        S3Helper s3ParamsHelper;
        S3Helper s3CloudFormationHelper;
        S3Helper s3ArtifactHelper;
        LambdaHelper lambdaHelper;

        public SoftwareDeliveryService()
        {
            s3ParamsHelper = new S3Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color,
                string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "parameters")
            );

            s3CloudFormationHelper = new S3Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color,
                string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "cloudformation")
             );

            s3ArtifactHelper = new S3Helper(
               GlobalVariables.Enviroment,
               GlobalVariables.Region,
               GlobalVariables.Color,
               GetArtifactS3Bucket()
            );

            lambdaHelper = new LambdaHelper(
             GlobalVariables.Enviroment,
             GlobalVariables.Region,
             GlobalVariables.Color);
        }

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
            await s3ParamsHelper.UploadFile("parameter.zip", new FileStream($"{tempPath}parameter.zip", FileMode.Open));
        }

        public async Task ExportCloudFormation()
        {
            var zipFilePath = GenerateInfraZip();

            await s3CloudFormationHelper.UploadFile(Path.GetFileName(zipFilePath), new FileStream(zipFilePath, FileMode.Open));
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
            await CopyApplicationZipFiles(applicationExportingLogs);
            await UpdateLambdaFunctions(applicationExportingLogs);
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

            var sourceFiles = s3ArtifactHelper.GetFolderFileKeys(s3ArtifactHelper.BucketName, sourceFolder);
            foreach (var sourceKey in sourceFiles)
            {
                if (sourceKey.Trim() == sourceFolder)
                    continue;
                var destinationKey = sourceKey.Replace(sourceFolder, detinationFolder);
                //await s3Helper.CopyFile(s3Helper.BucketName, sourceKey, s3Helper.BucketName, destinationKey);
                applicationExportingLogs.Add($"{DateTime.Now}:  {sourceKey} copied to {destinationKey}");
            }
        }

        private async Task<string> UpdateLambdaFunctions(List<string> applicationExportingLogs)
        {
            var result = "";
            try
            {
                var fileList = s3ArtifactHelper.GetBucketFileList("application/");
                foreach (var file in fileList)
                {
                    if (file.FullName.IndexOf("lambda") < 0)
                    {
                        continue;
                    }
                    var fileName = file.FullName.Replace("application/", string.Empty);
                    bool exsit;
                    var functionName = GetLambdaFunctionNameFromZipFileName(fileName, out exsit);
                    if (!exsit)
                    {
                        applicationExportingLogs.Add($"Warn: Can't find corresponing lambda function {functionName} for zip file '{file.FullName}'!");
                        continue;
                    }
                    //var response = await lambdaHelper.UpdateFunction(functionName, file.S3BucketName, file.FullName);
                    //applicationExportingLogs.Add($"Function {functionName} is updated. Description: {response}");
                    applicationExportingLogs.Add($"Function {functionName} is updated.");

                    applicationExportingLogs.Add(await UpdateLambdaFunctionVersion(functionName));
                }
                result = $"Updating lambda functions finished!";
            }
            catch (Exception ex)
            {
                result = $"Warn: {ex.Message}";
            }
            return result;
        }

        private async Task<string> UpdateLambdaFunctionVersion(string functionName)
        {
            var result = "";
            await Task.Run(() =>
            {
                try
                {
                    var tagName = "Version";
                    var tagValue = ReadAppVersion();
                    lambdaHelper.SetTag(functionName, tagName, tagValue);
                    var updatedVersion = lambdaHelper.ReadTag(functionName, tagName);
                    if (tagValue != updatedVersion)
                    {
                        result = $"Warn: Updating function {functionName} failed. Current version is {updatedVersion}. Desired version is {tagValue}.";
                    }
                    else
                    {
                        result = $"Updated function {functionName} to version {updatedVersion}";
                    }
                }
                catch (Exception ex)
                {
                    result = $"Warn: {ex.Message}";
                }
            });
            return result;
        }


        private string GetArtifactS3Bucket()
        {
            return string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "artifact");
        }

        private string GetLambdaFunctionNameFromZipFileName(string fileName, out bool exist)
        {
            LambdaHelper helper = new LambdaHelper(
             GlobalVariables.Enviroment,
             GlobalVariables.Region,
             GlobalVariables.Color);
            var functionName = string.Join("-", "SafeArrival", "Lambda",
                Path.GetFileNameWithoutExtension(fileName).Replace("lambda", ""),
                GlobalVariables.Enviroment, GlobalVariables.Color);
            if (helper.VerifyFunction(functionName))
                exist = true;
            else
                exist = false; ;
            return functionName;
        }

        private string ReadAppVersion()
        {
            return "1.5.0.0";
        }
    }
}
