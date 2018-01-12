using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.CloudFormation.Util;

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

        public async Task Get()
        {
            var x = await client.ListStacksAsync();
            var yReq = new ListStackSetsRequest() { Status= StackSetStatus.ACTIVE};
            var y = await client.ListStackSetsAsync(yReq);
            //client.
            //var y = await client.ListStackInstancesAsync();
            //var zReq = new Listst
            //var z = await client.ListStackResources(
        }
    }
}
