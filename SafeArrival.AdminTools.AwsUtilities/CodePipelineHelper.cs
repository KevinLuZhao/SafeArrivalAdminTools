using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CodePipeline;
using Amazon.CodePipeline.Model;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class CodePipelineHelper : AwsHelperBase
    {
        private AmazonCodePipelineClient client;

        public CodePipelineHelper(Model.Environment profile, string region, string color) : base(profile, region, color)
        {
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonCodePipelineClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        //public async Task<PipelineDeclaration> Get()
        public async Task<List<SA_PipelineSummary>> GetCodePipelineList()
        {
            var response = await client.ListPipelinesAsync();
            return ModelTransformer<PipelineSummary, SA_PipelineSummary>.
                TransformAwsModelListToSafeArrivalModelList(response.Pipelines).FindAll(o=>o.Name.Contains(environment.ToString()));
        }
    }
}
