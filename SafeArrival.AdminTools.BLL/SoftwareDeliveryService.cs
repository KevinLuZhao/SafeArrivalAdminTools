using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
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

        public string appsSourceFolder
        {
            get
            {
                return "application_source/";
            }
        }
        string appsDestinationFolder
        {
            get
            {
                return "application/";
            }
        }

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

        public async Task<string> ReadAppVersion(string folder)
        {
            var versionFile = await s3ArtifactHelper.DownloadFile($"{folder}version.txt");
            using (StreamReader sr = new StreamReader(versionFile))
            {
                return sr.ReadToEnd();
            }
        }

        public async Task<List<string>> GeneratePreviewData()
        {
            var ret = new List<string>();
            await Task.Run(() =>
            {
                var sourceFiles = s3ArtifactHelper.GetBucketFileList(appsSourceFolder);
                ret.Add($"ZIP files to be delivered on {GlobalVariables.Enviroment.ToUpper()}:{Environment.NewLine}");
                var nameLimit = 60;
                foreach (var file in sourceFiles)
                {
                    if (file.FullName.ToLower().IndexOf(".zip") < 0)
                        continue;
                    var fileName = Path.GetFileName(file.FullName);
                    var row = fileName;
                    int spaceNum = nameLimit - fileName.Length;
                    if (fileName == "api.zip")
                    {
                        var asd = spaceNum;
                    }
                    while (spaceNum > 0)
                    {
                        row += " ";
                        spaceNum--;
                    }
                    ret.Add($"{row}\t{file.LastModified}\t{file.Size}{Environment.NewLine}");
                }
            });

            ret.Add($"{Environment.NewLine}New version:{Environment.NewLine}");
            var version = await ReadAppVersion(appsSourceFolder);
            ret.Add($"{version}{Environment.NewLine}");

            ret.Add($"{Environment.NewLine}Current version:{Environment.NewLine}");
            version = await ReadAppVersion(appsDestinationFolder);
            ret.Add($"{version}{Environment.NewLine}");
            return ret;
        }

        public async Task DeliverApplications(List<string> applicationExportingLogs, bool copyFiles, bool updateLambdaFiles, bool updateLambdaVersions)
        {
            if (copyFiles)
            {
                UpdateApplicationExportingLogs(applicationExportingLogs, "---------------------- Copy application zip files ----------------------");
                var counter = await CopyApplicationZipFiles(applicationExportingLogs);
                UpdateApplicationExportingLogs(applicationExportingLogs,
                    $"{counter} ZIP application files are copied from " +
                    $"{s3ArtifactHelper.BucketName}/{appsSourceFolder} to " +
                    $"{s3ArtifactHelper.BucketName}/{appsDestinationFolder}");
            }
            if (updateLambdaFiles || updateLambdaVersions)
            {
                UpdateApplicationExportingLogs(applicationExportingLogs, "---------------------- Update lambda functions ----------------------");
                var counter = await UpdateLambdaFunctions(applicationExportingLogs, updateLambdaFiles, updateLambdaVersions);
                UpdateApplicationExportingLogs(applicationExportingLogs, $"{counter} lambda functions are updated");
            }
            UpdateApplicationExportingLogs(applicationExportingLogs, "---------------------- Applications delivery finished ----------------------");
        }

        private async Task<int> CopyApplicationZipFiles(List<string> applicationExportingLogs)
        {
            var sourceFiles = s3ArtifactHelper.GetFolderFileKeys(s3ArtifactHelper.BucketName, appsSourceFolder);
            var counter = 0;
            foreach (var sourceKey in sourceFiles)
            {
                if (sourceKey.Trim() == appsSourceFolder)
                    continue;
                var destinationKey = sourceKey.Replace(appsSourceFolder, appsDestinationFolder);
                try
                {
                    //await s3ArtifactHelper.CopyFile(s3ArtifactHelper.BucketName, sourceKey, s3ArtifactHelper.BucketName, destinationKey);
                }
                catch (Exception ex)
                {
                    UpdateApplicationException(applicationExportingLogs, ex);
                    continue;
                }
                UpdateApplicationExportingLogs(applicationExportingLogs, $"{sourceKey} copied to {destinationKey}");
                counter++;
            }
            return counter;
        }

        private async Task<int> UpdateLambdaFunctions(List<string> applicationExportingLogs, bool updateLambdaFiles, bool updateLambdaVersions)
        {
            var counter = 0;
            try
            {
                var fileList = s3ArtifactHelper.GetBucketFileList(appsDestinationFolder);
                foreach (var file in fileList)
                {
                    if (file.FullName.IndexOf("lambda") < 0)
                    {
                        continue;
                    }
                    var fileName = file.FullName.Replace(appsDestinationFolder, string.Empty);
                    bool exsit;
                    var lambda = GetLambdaFunctionFromZipFileName(fileName, out exsit);
                    if (!exsit)
                    {
                        UpdateApplicationExportingLogs(applicationExportingLogs, $"Warn: Expected lambda function '{FormatLambdaFunctionname(fileName)}' for zip file '{file.FullName}' can't be found!");
                        continue;
                    }
                    if (updateLambdaFiles)
                    {
                        try
                        {
                            //var response = await lambdaHelper.UpdateFunction(functionName, file.S3BucketName, file.FullName);
                            //UpdateApplicationExportingLogs(applicationExportingLogs, $"Function {functionName} is updated. Description: {response}");
                            UpdateApplicationExportingLogs(applicationExportingLogs, $"Function {lambda.FunctionName} is updated.");
                        }
                        catch (Exception ex)
                        {
                            UpdateApplicationException(applicationExportingLogs, ex);
                            continue;
                        }
                    }
                    if (updateLambdaVersions)
                    {
                        try
                        {
                            UpdateApplicationExportingLogs(applicationExportingLogs, await UpdateLambdaFunctionVersion(lambda));
                        }
                        catch (Exception ex)
                        {
                            UpdateApplicationException(applicationExportingLogs, ex);
                            continue;
                        }
                    }
                    counter++;
                }
            }
            catch (Exception ex)
            {
                UpdateApplicationException(applicationExportingLogs, ex);
            }
            return counter;
        }

        private async Task<string> UpdateLambdaFunctionVersion(SA_Lambda lambda)
        {
            var result = "";
            var tagName = "Version";
            var tagValue = await ReadAppVersion(appsDestinationFolder);
            await Task.Run(() =>
            {
                try
                {
                    //lambdaHelper.SetTag(lambda.FunctionArn, tagName, tagValue);
                    var updatedVersion = lambdaHelper.ReadTag(lambda.FunctionArn, tagName);
                    if (tagValue != updatedVersion)
                    {
                        result = $"Warn: Updating function {lambda.FunctionName} failed. Current version is {updatedVersion}. Desired version is {tagValue}.";
                    }
                    else
                    {
                        result = $"Updated function {lambda.FunctionName} to version {updatedVersion}";
                    }
                }
                catch (Exception ex)
                {
                    throw(ex);
                }
            });
            return result;
        }


        private string GetArtifactS3Bucket()
        {
            return string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "artifact");
        }

        private SA_Lambda GetLambdaFunctionFromZipFileName(string fileName, out bool exist)
        {
            LambdaHelper helper = new LambdaHelper(
             GlobalVariables.Enviroment,
             GlobalVariables.Region,
             GlobalVariables.Color);
            var functionName = FormatLambdaFunctionname(fileName);
            var lambda = lambdaHelper.GetFromFunctionName(functionName);
            exist = (lambda != null);
            return lambda;
        }

        private string FormatLambdaFunctionname(string fileName)
        {
            return string.Join("-", "SafeArrival", "Lambda",
                Path.GetFileNameWithoutExtension(fileName).Replace("lambda", ""),
                GlobalVariables.Enviroment, GlobalVariables.Color);
        }

        private void UpdateApplicationExportingLogs(List<string> applicationExportingLogs, string input)
        {
            applicationExportingLogs.Add($"{DateTime.Now}:  {input}");
        }

        private void UpdateApplicationException(List<string> applicationExportingLogs, Exception ex)
        {
            applicationExportingLogs.Add($"{DateTime.Now}:  Warn: {ex.Message}/n{ex.StackTrace}");
        }
    }
}
