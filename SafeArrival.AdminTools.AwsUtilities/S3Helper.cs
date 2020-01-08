using Amazon.S3;
using Amazon.S3.Model;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class S3Helper : AwsHelperBase
    {
        public string BucketName { get; set; }
        private AmazonS3Client client;

        //public S3Helper(string awsAccessKeyId, string awsSecretAccessKey, string serviceUrl, string bucketName)
        //{
        //    //AwsAccessKeyId = awsAccessKeyId;
        //    //AwsSecretAccessKey = awsSecretAccessKey;
        //    //ServiceUrl = serviceUrl;
        //    //BucketName = bucketName;

        //    if (string.IsNullOrEmpty(serviceUrl))
        //    {
        //        client = new AmazonS3Client();
        //    }
        //    else
        //    {
        //        var s3Config = new AmazonS3Config { ServiceURL = serviceUrl };
        //        //client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, s3Config);
        //        client = new AmazonS3Client(
        //            CredentiaslManager.GetDynamoDbCredential(), s3Config);
        //    }
        //    BucketName = bucketName;
        //}

        public S3Helper(string profile, string region, string color, string bucketName) : base(profile, region, color)
        {
            //Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials(profile.ToString());
            //client = new AmazonS3Client(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonS3Client(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
            BucketName = bucketName;
        }

        public async Task CopyFile(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey)
        {
            var response = await client.CopyObjectAsync(sourceBucket, sourceKey, destinationBucket, destinationKey);
            //response.
        }

        /// <summary>
        /// Upload the File to AWS S3
        /// </summary>
        /// <param name="fullFileName">File Name with Sub Path (if have)</param>
        /// <param name="fileStream"></param>
        public async Task UploadFile(string fullFileName, Stream fileStream)
        {
            if (fullFileName == null)
            {
                throw new ArgumentNullException(nameof(fullFileName));
            }
            if (fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            var request = new PutObjectRequest
            {
                BucketName = BucketName,
                Key = fullFileName,
                InputStream = fileStream,

            };

            PutObjectResponse response = await client.PutObjectAsync(request);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK &&
                response.HttpStatusCode != System.Net.HttpStatusCode.Created &&
                response.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
            {
                string error = string.Format("Upload the File to Aws return HttpCode: {0}", response.HttpStatusCode);
                var ex = new Exception(error);
                ex.Data.Add("Response", response);
                throw ex;
            }
        }

        /// <summary>
        /// Download a file with full file name and return stream.
        /// </summary>
        /// <param name="fullFileName">File Name with Sub Path (if have)</param>
        /// <returns></returns>
        public async Task<Stream> DownloadFile(string fullFileName)
        {
            if (fullFileName == null)
            {
                throw new ArgumentNullException(nameof(fullFileName));
            }

            var request = new GetObjectRequest
            {
                BucketName = BucketName,
                Key = fullFileName
            };

            GetObjectResponse response = await client.GetObjectAsync(request);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                string error = string.Format("Download the File from Aws return HttpCode: {0}", response.HttpStatusCode);
                var ex = new Exception(error);
                ex.Data.Add("Response", response);

            }
            return response.ResponseStream;
        }

        /// <summary>
        /// Delete a file from AWS S3
        /// </summary>
        /// <param name="fullFileName"></param>
        public async Task DeleteFile(string fullFileName)
        {
            if (fullFileName == null)
            {
                throw new ArgumentNullException(nameof(fullFileName));
            }

            var request = new DeleteObjectRequest
            {
                BucketName = BucketName,
                Key = fullFileName
            };
            DeleteObjectResponse response = await client.DeleteObjectAsync(request);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK &&
                response.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
            {
                string error = string.Format("Delete the File from Aws return HttpCode: {0}", response.HttpStatusCode);
                var ex = new Exception(error);
                ex.Data.Add("Response", response);
                throw ex;
            }
        }

        public List<SA_S3Object> GetBucketFileList(string prefix = "")
        {
            var output = new List<SA_S3Object>();
            var request = new ListObjectsRequest();
            request.BucketName = BucketName;
            ListObjectsResponse listResponse;
            do
            {
                listResponse = client.ListObjects(request);
                foreach (S3Object obj in listResponse.S3Objects)
                {
                    output.Add(new SA_S3Object(obj.Key, BucketName, obj.LastModified, obj.Size));
                }
                request.Marker = listResponse.NextMarker;
            } while (listResponse.IsTruncated);
            //output.Sort();
            return output;
        }

        public string GetBucketFileListHtml()
        {
            StringBuilder output = new StringBuilder();
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = BucketName;
            ListObjectsResponse listResponse;
            do
            {
                // Get a list of objects
                listResponse = client.ListObjects(request);
                foreach (S3Object obj in listResponse.S3Objects)
                {
                    output.AppendFormat("<li>{0}</li>", obj.Key);
                }

                // Set the marker property
                request.Marker = listResponse.NextMarker;
            } while (listResponse.IsTruncated);
            return output.ToString();
        }

        public async Task PutNotification(string id = "", string functionArn = "", string s3Event = "")
        {
            LambdaFunctionConfiguration lambdaConfig = null;
            if (id!="")
            {
                var events = new List<EventType>();
                events.Add(new EventType(s3Event));
                lambdaConfig = new LambdaFunctionConfiguration()
                {
                    FunctionArn = functionArn,
                    Id = id,
                    Events = events
                };
            }
            
            var request = new PutBucketNotificationRequest()
            {
                BucketName = BucketName,
                LambdaFunctionConfigurations = new List<LambdaFunctionConfiguration>() { lambdaConfig }
            };
            var response = await client.PutBucketNotificationAsync(request);
        }

        public async Task GetBuckets()
        {
            var request = new ListBucketsRequest();
            var response = await client.ListBucketsAsync();
            //return response.Buckets;
        }
    }
}
