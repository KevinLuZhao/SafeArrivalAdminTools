using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class CloudFormationHelper : AwsHelperBase
    {
        AmazonCloudFormationClient client;
        public CloudFormationHelper(Model.Environment profile, string region, string color) : base(profile, region, color)
        {
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonCloudFormationClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<List<SA_Stack>> GetStackList()
        {
            var saStacks = new List<SA_Stack>();
            var response = await client.DescribeStacksAsync();
            foreach (Stack awsStack in response.Stacks)
            {
                var saStack = ConvertStack(awsStack);
                //Here is a problem, Level 1 does not have color, but we can't exclude color because we 
                //dont want to select both green and blue.
                if (saStack.DisplayName.Contains(environment.ToString()) && saStack.DisplayName.Contains(color))
                    saStacks.Add(saStack);
            }
            saStacks.Sort((a, b) => b.CreationTime.CompareTo(a.CreationTime));
            return saStacks.ToList();
        }

        public async Task<string> DeleteStack(string stackName, AsyncCallback callBack)
        {
            var getRequest = new DescribeStacksRequest()
            { StackName = stackName };
            var request = new DeleteStackRequest()
            {
                StackName = stackName
            };
            var response = await client.DeleteStackAsync(request);
            await Task.Delay(1000);
            callBack(null);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                return response.HttpStatusCode.ToString();
            // client.describestack
            while (true)
            {
                await Task.Delay(5000);
                try
                {
                    var getResponse = await client.DescribeStacksAsync(getRequest);
                    if (getResponse.Stacks == null || getResponse.Stacks.Count == 0)
                        return "DELETE_COMPLETE";
                    var stack = getResponse.Stacks[0];
                    if (stack.StackStatus == StackStatus.DELETE_FAILED)
                        return "DELETE_FAILED";
                    else if (stack.StackStatus == StackStatus.DELETE_COMPLETE)
                        return "DELETE_COMPLETE";
                    else
                        continue;
                }
                catch (Amazon.CloudFormation.AmazonCloudFormationException ex)
                {
                    if (ex.Message.Trim() == $"Stack with id {stackName} does not exist".Trim())
                        return "DELETE_COMPLETE";
                    throw ex;
                }               
            }
        }

        public async Task<string> CreateStack(string path, string stackName, 
            string templateContent, List<KeyValuePair<string, string>> saParameters)
        {
            List<Parameter> awsParameters = new List<Parameter>();
            foreach (var saParam in saParameters)
            {
                Parameter awsParam = new Parameter();
                awsParam.ParameterKey = saParam.Key;
                awsParam.ParameterValue = saParam.Value;
                awsParameters.Add(awsParam);
            }
            var request = new CreateStackRequest()
            {
                StackName = stackName,
                TemplateBody = templateContent,
                Parameters = awsParameters
            };
            var response = await client.CreateStackAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return response.StackId;
            else
                return response.HttpStatusCode.ToString();
        }

        private SA_Stack ConvertStack(Stack awsStack)
        {
            var saStack = ModelTransformer<Stack, SA_Stack>.
                TransformAwsModelToSafeArrivalModel(awsStack);
            //saListener.Rule = awsListener.DefaultActions[0].Type.ToString() + 
            //    " to " + awsListener.DefaultActions[0].TargetGroupArn;
            if (awsStack.Tags.Count > 0)
            {
                saStack.DisplayName = awsStack.Tags.Find(o => o.Key == "Name").Value;
            }
            else
            {
                saStack.DisplayName = awsStack.StackName;
            }
            return saStack;
        }
    }
}
