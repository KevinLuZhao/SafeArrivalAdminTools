using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using SafeArrival.AdminTools.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class AutoScalingHelper : AwsHelperBase
    {
        //public string Environment { get; }
        private AmazonAutoScalingClient client;
        public string profile;

        public AutoScalingHelper(string profile, string region, string color) : base(profile, region, color)
        {
            this.profile = profile;
            client = new AmazonAutoScalingClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<List<SA_AutoScalingGroup>> GetEnvironmentAutoScalingGroupList(bool ignorColor = false)
        {
            var lstSaGroups = new List<SA_AutoScalingGroup>();
            var response = await client.DescribeAutoScalingGroupsAsync();
            var lstGroups = ignorColor ?
                response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(environment.ToString()) >= 0) :
                response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(environment + "-" + color) >= 0);
            //var lstGroups = response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.Substring(0, o.Tags[0].Value.IndexOf("-")) == environment.ToString());
            var jumpBox = response.AutoScalingGroups.Find(
                o => o.Tags[0].Value.IndexOf(environment.ToString()) >= 0 && o.Tags[0].Value.IndexOf("Jump") > 0);
            if (jumpBox != null && lstGroups.Find(o => o.Tags[0].Value.IndexOf("Jump") > 0) == null)
                lstGroups.Add(jumpBox);
            foreach (var group in lstGroups)
            {
                var saGroup = new SA_AutoScalingGroup()
                {
                    AutoScalingGroupName = group.AutoScalingGroupName,
                    AutoScalingGroupARN = group.AutoScalingGroupARN,
                    Name = group.Tags[0].Value,
                    //Status = group.Status,
                    MaxSize = group.MaxSize,
                    MinSize = group.MinSize,
                    DesiredCapacity = group.DesiredCapacity,
                    CreatedTime = group.CreatedTime,
                    RunningInstances = group.Instances.Count
                };
                lstSaGroups.Add(saGroup);
            }
            var results = lstSaGroups.OrderBy(o => o.Name).ToList();
            return results;
        }

        public async Task<List<SA_AutoScalingGroup>> GetAllAutoScalingGroupList(string region = null)
        {
            if (region != null)
            {
                client = new AmazonAutoScalingClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
            }

            var lstSaGroups = new List<SA_AutoScalingGroup>();
            var response = await client.DescribeAutoScalingGroupsAsync();
            var lstGroups = response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(environment.ToString()) >= 0);

            foreach (var group in lstGroups)
            {
                var saGroup = new SA_AutoScalingGroup()
                {
                    AutoScalingGroupName = group.AutoScalingGroupName,
                    AutoScalingGroupARN = group.AutoScalingGroupARN,
                    Name = group.Tags[0].Value,
                    //Status = group.Status,
                    MaxSize = group.MaxSize,
                    MinSize = group.MinSize,
                    DesiredCapacity = group.DesiredCapacity,
                    CreatedTime = group.CreatedTime,
                    RunningInstances = group.Instances.Count
                };
                lstSaGroups.Add(saGroup);
            }
            var results = lstSaGroups.OrderBy(o => o.Name).ToList();
            return results;
        }

        public List<SA_AsgEc2Map> GetAsgEc2Maps(string region = null)
        {
            var rets = new List<SA_AsgEc2Map>();
            if (region != null)
            {
                client = new AmazonAutoScalingClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
            }

            var lstSaGroups = new List<SA_AutoScalingGroup>();
            var response = client.DescribeAutoScalingGroups();
            var lstGroups = response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(environment.ToString()) >= 0);

            foreach (var group in lstGroups)
            {
                if (group.Instances != null)
                {
                    foreach (var instance in group.Instances)
                    {
                        rets.Add(new SA_AsgEc2Map()
                        {
                            AsgName = group.Tags[0].Value,
                            AutoScalingGroupName = group.AutoScalingGroupName,
                            InstanceId = instance.InstanceId
                        });
                    }
                }
            }
            return rets;
        }

        public async Task StopScalingGroup(string groupName)
        {
            UpdateAutoScalingGroupRequest request = new UpdateAutoScalingGroupRequest();
            request.MaxSize = request.MinSize = request.DesiredCapacity = 0;
            request.AutoScalingGroupName = groupName;
            await client.UpdateAutoScalingGroupAsync(request);
        }

        public async Task ChangeScalingGroup(string groupName, AutoScalingGroupSettings settings, string region = null)
        {
            if (region != null)
            {
                client = new AmazonAutoScalingClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
            }
            UpdateAutoScalingGroupRequest request = new UpdateAutoScalingGroupRequest();
            request.MaxSize = settings.MaxSize;
            request.MinSize = settings.MinSize;
            request.DesiredCapacity = settings.DesiredCapacity;
            request.AutoScalingGroupName = groupName;
            await client.UpdateAutoScalingGroupAsync(request);
        }

        public async Task AttachLoadBalancerTargetGroups(SA_AutoScalingGroup autoScalingGroup, List<string> targetGroupARNs)
        {
            var request = new AttachLoadBalancerTargetGroupsRequest()
            {
                AutoScalingGroupName = autoScalingGroup.AutoScalingGroupName,
                TargetGroupARNs = targetGroupARNs
            };
            //client.DetachLoadBalancerTargetGroupsAsync()
            await client.AttachLoadBalancerTargetGroupsAsync(request);
        }

        public async Task ClearAutoScalingGroupAttachedTargetGroups(SA_AutoScalingGroup autoScalingGroup)
        {
            var getRequest = new DescribeLoadBalancerTargetGroupsRequest()
            {
                AutoScalingGroupName = autoScalingGroup.AutoScalingGroupName
            };
            var getResponse = await client.DescribeLoadBalancerTargetGroupsAsync(getRequest);
            if (getResponse.LoadBalancerTargetGroups.Count == 0)
                return;

            var removeRequest = new DetachLoadBalancerTargetGroupsRequest()
            {
                AutoScalingGroupName = autoScalingGroup.AutoScalingGroupName,
                TargetGroupARNs = getResponse.LoadBalancerTargetGroups.ConvertAll(o => o.LoadBalancerTargetGroupARN)
            };
            await client.DetachLoadBalancerTargetGroupsAsync(removeRequest);
        }
    }
}
