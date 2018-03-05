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

        public CodePipelineHelper(string profile, string region, string color) : base(profile, region, color)
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
            var lst = ModelTransformer<PipelineSummary, SA_PipelineSummary>.
                TransformAwsModelListToSafeArrivalModelList(response.Pipelines).FindAll(o =>
                (o.Name.IndexOf($"{environment.ToString()}-") == 0 &&
                (o.Name.Contains(color) || (!o.Name.Contains("green") && (!o.Name.Contains("blue"))))));
            lst.Sort((a, b) => b.Created.CompareTo(a.Created));
            //foreach (var codePipeline in lst)
            //{
            //    var request = new ListPipelineExecutionsRequest() { PipelineName = codePipeline.Name };
            //    var x = client.ListPipelineExecutions(request);
            //}
            return lst.ToList();
        }

        public async Task<bool> GetCodePipelineStageTransitionStatus(string codePipelineName, string stageName)
        {
            var request = new GetPipelineStateRequest() { Name = codePipelineName };
            var response = await client.GetPipelineStateAsync(request);
            return response.StageStates.Find(o => o.StageName == stageName).InboundTransitionState.Enabled;
        }

        public async Task DisableTransition(string pipelineName, string stageName, string reason, string stageTransitionType = "")
        {
            var request = new DisableStageTransitionRequest()
            {
                PipelineName = pipelineName,
                StageName = stageName,
                Reason = reason
            };
            stageTransitionType = string.IsNullOrEmpty(stageTransitionType) ? "Inbound" : stageTransitionType;
            request.TransitionType = StageTransitionType.FindValue(stageTransitionType);
            var response = await client.DisableStageTransitionAsync(request);
        }

        public async Task EnableTransition(string pipelineName, string stageName, string stageTransitionType = "")
        {
            var request = new EnableStageTransitionRequest()
            {
                PipelineName = pipelineName,
                StageName = stageName
            };
            stageTransitionType = string.IsNullOrEmpty(stageTransitionType) ? "Inbound" : stageTransitionType;
            request.TransitionType = StageTransitionType.FindValue(stageTransitionType);
            var response = await client.EnableStageTransitionAsync(request);
        }
    }
}
