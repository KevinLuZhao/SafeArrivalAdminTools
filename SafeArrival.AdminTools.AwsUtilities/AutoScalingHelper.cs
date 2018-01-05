using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using SafeArrival.AdminTools.Model;
using Amazon.Runtime;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class AutoScalingHelper : AwsHelperBase
    {
        //public string Environment { get; }
        private AmazonAutoScalingClient client;

        public AutoScalingHelper(Model.Environment profile, string region, string color) : base(profile, region, color)
        {
            client = new AmazonAutoScalingClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<List<SA_AutoScalingGroup>> GetAutoScalingGroupList(bool ignorColor = false)
        {
            var lstSaGroups = new List<SA_AutoScalingGroup>();
            var response = await client.DescribeAutoScalingGroupsAsync();
            var lstGroups = ignorColor ?
                response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(environment.ToString()) >= 0) :
                response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(environment + "-" + color) >= 0);
            var jumpBox = response.AutoScalingGroups.Find(
                o => o.Tags[0].Value.IndexOf(environment.ToString()) >= 0 && o.Tags[0].Value.IndexOf("Jump") > 0);
            if (jumpBox != null)
                lstGroups.Add(jumpBox);
            foreach (var group in lstGroups)
            {
                var saGroup = new SA_AutoScalingGroup()
                {
                    AutoScalingGroupName = group.AutoScalingGroupName,
                    AutoScalingGroupARN = group.AutoScalingGroupARN,
                    Name = group.Tags[0].Value,
                    Status = group.Status,
                    MaxSize = group.MaxSize,
                    MinSize = group.MinSize,
                    DesiredCapacity = group.DesiredCapacity,
                    CreatedTime = group.CreatedTime,
                    RunningInstances = group.Instances.Count
                };
                lstSaGroups.Add(saGroup);
            }
            return lstSaGroups;
        }

        public async Task StopScalingGroup(string groupName)
        {
            UpdateAutoScalingGroupRequest request = new UpdateAutoScalingGroupRequest();
            request.MaxSize = request.MinSize = request.DesiredCapacity = 0;
            request.AutoScalingGroupName = groupName;
            await client.UpdateAutoScalingGroupAsync(request);
        }

        public async Task ChangeScalingGroup(string groupName, AutoScalingGroupSettings settings)
        {
            UpdateAutoScalingGroupRequest request = new UpdateAutoScalingGroupRequest();
            request.MaxSize = settings.MaxSize;
            request.MinSize = settings.MinSize;
            request.DesiredCapacity = settings.DesiredCapacity;
            request.AutoScalingGroupName = groupName;
            await client.UpdateAutoScalingGroupAsync(request);
        }

        public async Task AttachLoadBalancerTargetGroups(string autoScalingGroupName, List<string> targetGroupARNs)
        {
            var request = new AttachLoadBalancerTargetGroupsRequest()
            {
                AutoScalingGroupName = autoScalingGroupName,
                TargetGroupARNs = targetGroupARNs
            };
            await client.AttachLoadBalancerTargetGroupsAsync(request);
        }
    }
}
