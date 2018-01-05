using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.AwsUtilities;

namespace SafeArrival.AdminTools.BLL
{
    public class SwitchDeploymentService
    {
        LoadBalancerHelper loadBalancerHelper = new LoadBalancerHelper(
            GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
        public async Task GenerateExternalLoadBalancers()
        {
            var scalingGroupHelper = new AutoScalingHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var apps = Enum.GetNames(typeof(ApplicationServer)).ToList();
            apps.Remove("JumpBox");
            var colors = Enum.GetNames(typeof(Color)).ToList();
            var sGroups = await scalingGroupHelper.GetAutoScalingGroupList(true);
            sGroups.Remove(sGroups.Find(o => o.Name.IndexOf("Jump") >= 0));
            var ec2Helper = new EC2Helper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var vpc = await ec2Helper.GetVPC();
            var subnets = await ec2Helper.GetSubnetList();
            var securirtyGroups = await ec2Helper.GetSecurityGroupList();
            foreach (var appName in apps)
            {
                var tGroups = new List<SA_TargetGroup>();
                var loadBalancerArn = await loadBalancerHelper.CreateLoadBalancer(
                    $"{GlobalVariables.Enviroment.ToString()}-{appName}-ALB",
                    securirtyGroups.FindAll(o =>
                        o.DisplayName.Contains(appName) &&
                        o.DisplayName.Contains(GlobalVariables.Enviroment.ToString()) &&
                        o.Description == "Load Balancer Security Group").Select(o => o.GroupId).ToList(),
                    subnets.FindAll(o => o.DisplayName.Contains("Public")).Select(o => o.SubnetId).ToList());
                foreach (var color in colors)
                {
                    var tGroup = await loadBalancerHelper.CreateTargetGroup(
                        $"{GlobalVariables.Enviroment.ToString()}-{color}-{appName}-TG",
                        vpc.VpcId, "HTTP", 80);
                    await scalingGroupHelper.AttachLoadBalancerTargetGroups(
                        sGroups.Find(o => o.Name.Contains($"{GlobalVariables.Enviroment}-{color}")).Name,
                        new List<string>() { tGroup.TargetGroupArn });
                    tGroups.Add(tGroup);
                }

                await loadBalancerHelper.CreatListener(loadBalancerArn,
                    tGroups.Find(o => o.TargetGroupName.Contains(Color.green.ToString())).TargetGroupArn,
                    "HTTP", 80);
                await loadBalancerHelper.CreatListener(loadBalancerArn,
                    tGroups.Find(o => o.TargetGroupName.Contains(Color.green.ToString())).TargetGroupArn,
                    "HTTPS", 443,
                    "arn:aws:acm:us-east-2:125237747044:certificate/abb607ce-4090-45cb-bf29-923e08106664");
                await loadBalancerHelper.CreatListener(loadBalancerArn,
                    tGroups.Find(o => o.TargetGroupName.Contains(Color.blue.ToString())).TargetGroupArn,
                    "HTTP", 8080);
                await loadBalancerHelper.CreatListener(loadBalancerArn,
                    tGroups.Find(o => o.TargetGroupName.Contains(Color.blue.ToString())).TargetGroupArn,
                    "HTTPS", 4343,
                    "arn:aws:acm:us-east-2:125237747044:certificate/abb607ce-4090-45cb-bf29-923e08106664");
            }
        }

        public async Task<List<ApplicationLoadBalancerModel>> GetApplicationLoadBalancers()
        {
            var applicationLoadBalancers = new List<ApplicationLoadBalancerModel>();
            var loadBalancers = await loadBalancerHelper.GetLoadBalancerList();
            foreach (var loadBalancer in loadBalancers)
            {
                var applicationLoadBalancer = new ApplicationLoadBalancerModel();
                applicationLoadBalancer.LoadBalancer = loadBalancer;
                applicationLoadBalancer.Listeners = await loadBalancerHelper.GetListenerList(loadBalancer.LoadBalancerArn);
                applicationLoadBalancers.Add(applicationLoadBalancer);
            }
            return applicationLoadBalancers;
        }
    }
}
