using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.ElasticLoadBalancingV2;
using Amazon.ElasticLoadBalancingV2.Model;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class LoadBalancerHelper : AwsHelperBase
    {
        private AmazonElasticLoadBalancingV2Client client;

        public LoadBalancerHelper(Model.Environment profile, string region, string color) : base(profile, region, color)
        {
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonElasticLoadBalancingV2Client(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<string> CreateLoadBalancer(
            string name, List<string> securityGroups, List<string> subnets)
        {
            var request = new CreateLoadBalancerRequest()
            {
                Name = name,
                SecurityGroups = securityGroups,
                Subnets = subnets,
                IpAddressType = IpAddressType.Ipv4,
                Scheme = LoadBalancerSchemeEnum.InternetFacing,
                //Type = LoadBalancerTypeEnum.Application
            };
            var response = await client.CreateLoadBalancerAsync(request);

            return response.LoadBalancers[0].LoadBalancerArn;
        }

        public async Task<List<SA_LoadBalancer>> GetLoadBalancerList()
        {
            var request = new DescribeLoadBalancersRequest();
            var response = await client.DescribeLoadBalancersAsync(request);
            var lstLoadBalancers = ModelTransformer<LoadBalancer, SA_LoadBalancer>.
                TransformAwsModelListToSafeArrivalModelList(response.LoadBalancers);
            lstLoadBalancers = lstLoadBalancers.FindAll(o => o.LoadBalancerName.Contains($"{environment}-"));
            return lstLoadBalancers;
        }

        public async Task<SA_TargetGroup> CreateTargetGroup(
            string name, string vpcId, string protocol, int port)
        {
            var request = new CreateTargetGroupRequest()
            {
                Name = name,
                VpcId = vpcId,
                Protocol = new ProtocolEnum(protocol),
                Port = 80
            };
            var response = await client.CreateTargetGroupAsync(request);

            return ModelTransformer<TargetGroup, SA_TargetGroup>.TransformAwsModelToSafeArrivalModel(response.TargetGroups[0]);
        }

        public async Task<List<SA_TargetGroup>> GetTargetGroupList(bool ignorColor = false)
        {
            var request = new DescribeTargetGroupsRequest();
            var response = await client.DescribeTargetGroupsAsync(request);
            var lstTargetGroups = ModelTransformer<TargetGroup, SA_TargetGroup>.
                TransformAwsModelListToSafeArrivalModelList(response.TargetGroups);
            if (ignorColor)
                lstTargetGroups = lstTargetGroups.FindAll(o => o.TargetGroupName.Contains($"{environment}-"));
            else
                lstTargetGroups = lstTargetGroups.FindAll(o => o.TargetGroupName.Contains($"{environment}-{color}-"));
            return lstTargetGroups;
        }

        public async Task<SA_Listener> CreatListener(
            string loadBalancerArn, string targetGroupArn,
            string strProtocol, int port, string certificateArn = "")
        {
            var actions = new List<Amazon.ElasticLoadBalancingV2.Model.Action>();
            var action = new Amazon.ElasticLoadBalancingV2.Model.Action();
            action.Type = ActionTypeEnum.Forward;
            action.TargetGroupArn = targetGroupArn;
            actions.Add(action);
            var protocol = new ProtocolEnum(strProtocol);
            var request = new CreateListenerRequest
            {
                DefaultActions = actions,
                Protocol = protocol,
                Port = port,
                LoadBalancerArn = loadBalancerArn,
            };
            if (!string.IsNullOrEmpty(certificateArn))
            {
                var certificate = new Certificate();
                certificate.CertificateArn = certificateArn;
                //certificate.IsDefault = true;
                request.Certificates = new List<Certificate>() { certificate };
            }

            var response = await client.CreateListenerAsync(request);
            var listener = response.Listeners[0];

            return ConvertListener(response.Listeners[0]);
        }

        public async Task ChangeListenerTargets(string listenerArn, string targetGroupArn)
        {
            var requestGet = new DescribeListenersRequest()
            {
                ListenerArns = new List<string>() { listenerArn }
            };
            var responseGet = await client.DescribeListenersAsync(requestGet);
            Listener listener = responseGet.Listeners[0];
            listener.DefaultActions[0].TargetGroupArn = targetGroupArn;
            var request = new ModifyListenerRequest()
            {
                ListenerArn = listenerArn,
                DefaultActions = listener.DefaultActions
            };
            await client.ModifyListenerAsync(request);
        }

        public async Task<List<SA_Listener>> GetListenerList(string loadBalancerArn)
        {
            var request = new DescribeListenersRequest()
            {
                LoadBalancerArn = loadBalancerArn
            };
            var response = await client.DescribeListenersAsync(request);
            var lstListeners = new List<SA_Listener>();
            foreach (var listener in response.Listeners)
            {
                lstListeners.Add(ConvertListener(listener));
            }
            return lstListeners;
        }

        private SA_Listener ConvertListener(Listener awsListener)
        {
            var saListener = ModelTransformer<Listener, SA_Listener>.
                TransformAwsModelToSafeArrivalModel(awsListener);
            //saListener.Rule = awsListener.DefaultActions[0].Type.ToString() + 
            //    " to " + awsListener.DefaultActions[0].TargetGroupArn;
            saListener.TargetArn = awsListener.DefaultActions[0].TargetGroupArn;
            return saListener;
        }
    }
}
