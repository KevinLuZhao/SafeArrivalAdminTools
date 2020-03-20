using Amazon.Lambda;
using Amazon.Lambda.Model;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class LambdaHelper : AwsHelperBase
    {
        private AmazonLambdaClient client;

        public LambdaHelper(string profile, string region, string color) : base(profile, region, color)
        {
            client = new AmazonLambdaClient(
                CredentiaslManager.GetCredential(environment),
                AwsCommon.GetRetionEndpoint(region));
        }

        public SA_Lambda GetFromFunctionName(string functionName)
        {
            try
            {
                var response = client.GetFunction(functionName);
                var result = new SA_Lambda()
                {
                    FunctionArn = response.Configuration.FunctionArn,
                    FunctionName = response.Configuration.FunctionName,
                    LastModified = DateTime.Parse(response.Configuration.LastModified),
                    Tags = response.Tags
                };
                return result;
            }
            catch (ResourceNotFoundException)
            {
                return null;
            }
        }

        public async Task<List<string>> GetFunctionList()
        {
            var response = await client.ListFunctionsAsync();
            return null;
        }

        //Unfinished
        public void SetEventTrigger()
        {
            var lstLambdaFuncs = client.ListFunctions();
            var request = new GetEventSourceMappingRequest() { };
            var policy = client.GetEventSourceMapping(request);
        }

        public async Task AddPolicy(string account)
        {
            try
            {
                var request = new AddPermissionRequest()
                {
                    Action = "lambda:InvokeFunction",
                    FunctionName = $"arn:aws:lambda:us-east-2:125237747044:function:SafeArrival-SIS-infra-green",
                    Principal = "s3.amazonaws.com",
                    SourceArn = $"arn:aws:s3:::safe-arrival-{region}-{environment}-sisbucket",
                    StatementId = "SIS_Lambda_Trigger_Permission",
                    SourceAccount = account
                };
                var response = await client.AddPermissionAsync(request);
            }
            catch (Amazon.Lambda.Model.ResourceConflictException)
            {
                //Policy created, skip.
                ;
            }
        }

        public string ReadTag(string lambdaFunctionName, string tagName)
        {
            string tagValue;
            var lstLambdaFuncs = client.ListFunctions();

            var request = new ListTagsRequest();
            request.Resource = lstLambdaFuncs.Functions[0].FunctionArn;
            var tagsResponse = client.ListTags(request);
            if (tagsResponse.Tags.TryGetValue(tagName, out tagValue))
            {
                return tagValue;
            }
            return null;
        }

        public void SetTag(string lambdaFunctionName, string tagName, string tagValue)
        {
            var request = new TagResourceRequest();
            request.Resource = lambdaFunctionName;
            request.Tags.Add(tagName, tagValue);
            client.TagResource(request);
        }

        public async Task<string> UpdateFunction(string functionName, string s3Bucket, string s3Key)
        {
            var request = new UpdateFunctionCodeRequest()
            {
                FunctionName = functionName,
                S3Bucket = s3Bucket,
                S3Key = s3Key
            };
            var response = await client.UpdateFunctionCodeAsync(request);
            return response.Description.Trim();
        }

        public void UpdateFunctionDescription(string lambdaFunctionName, string description)
        {
            var request = new UpdateFunctionConfigurationRequest();
            request.FunctionName = lambdaFunctionName;
            request.Description = description;
            var response = client.UpdateFunctionConfiguration(request);
        }
    }
}
