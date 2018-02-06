using Amazon.Lambda;
using Amazon.Lambda.Model;
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
        public void SetEventTrigger()
        {
            //S3Helper s3Helper = new S3Helper();
            var lstLambdaFuncs = client.ListFunctions();


            //var request = new CreateEventSourceMappingRequest()
            //{ 
            //    FunctionName = lstLambdaFuncs.Functions[0].FunctionName,
            //    Enabled = true,
            //    StartingPosition = EventSourcePosition.AT_TIMESTAMP,
            //    EventSourceArn = $"arn:aws:s3:::safe-arrival-{region}-{environment.ToString()}-sisbucket"
            //};
            var request = new GetEventSourceMappingRequest() { };
            var policy = client.GetEventSourceMapping(request);
            //client.CreateEventSourceMapping(request);

            CreateFunctionRequest re = new CreateFunctionRequest();
            var x = client.CreateFunction(re);
            //x.
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
    }
}
