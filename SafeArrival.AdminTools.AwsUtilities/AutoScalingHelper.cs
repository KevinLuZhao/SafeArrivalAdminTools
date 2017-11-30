using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using SafeArrival.AdminTools.Model;
using Amazon.Runtime;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class AutoScalingHelper
    {
        public string Environment { get; }
        private AmazonAutoScalingClient client;

        public AutoScalingHelper(Model.Environment profile, string region)
        {
            client = new AmazonAutoScalingClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<List<AwsAutoScalingGroup>> GetAutoScalingGroupList()
        {
            var lstSaGroups = new List<AwsAutoScalingGroup>();
            var response = await client.DescribeAutoScalingGroupsAsync();
            var lstGroups = response.AutoScalingGroups.FindAll(o => o.Tags[0].Value.IndexOf(GlobalVariables.Enviroment.ToString()) >= 0);
            foreach (var group in lstGroups)
            {
                var saGroup = new AwsAutoScalingGroup()
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
    }
}
