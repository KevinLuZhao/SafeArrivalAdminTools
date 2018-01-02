using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class LambdaHelper:AwsHelperBase
    {
        private AmazonLambdaClient client;

        public LambdaHelper(Model.Environment profile, string region, string color) : base(profile, region, color)
        {
            client = new AmazonLambdaClient(
                CredentiaslManager.GetCredential(environment),
                AwsCommon.GetRetionEndpoint(region));
        }
        public void SetEventTrigger()
        {
            //S3Helper s3Helper = new S3Helper();
            var lstLambdaFuncs = client.ListFunctions();
            var request = new CreateEventSourceMappingRequest()
            { 
                 FunctionName = lstLambdaFuncs.Functions[0].FunctionName,
                  Enabled= true,
                   EventSourceArn = "arn:aws:s3:::safe-arrival-us-east-2-production-sisbucket"

            };
            //var policy = client.GetEventSourceMapping(request);
            
        }
    }
}
