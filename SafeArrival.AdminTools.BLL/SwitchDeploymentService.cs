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
        const int PRODUCTION_HTTP_PORT = 80;
        const int PRODUCTION_HTTPS_PORT = 443;
        const int PRE_PRODUCTION_HTTP_PORT = 8080;
        const int PRE_PRODUCTION_HTTPS_PORT = 4343;
        public async Task GenerateExternalLoadBalancers()
        {
            ALBHelper loadBalancerHelper = new ALBHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var scalingGroupHelper = new AutoScalingHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var apps = Enum.GetNames(typeof(ApplicationServer)).ToList();
            apps.Remove("JumpBox");
            apps.Remove("Worker");
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
                var newSGs = new List<string>();
                var oldSGs = securirtyGroups.FindAll(o =>
                        o.DisplayName.Contains(appName) &&
                        o.DisplayName.Contains(GlobalVariables.Enviroment) &&
                        o.Description == "Load Balancer Security Group").Select(o => o.GroupId).ToList();
                //foreach (string oldSG in oldSGs)
                //{
                //    await ec2Helper.CopySecurityGroup(oldSG, 
                //        $"{GlobalVariables.Enviroment}-{GlobalVariables.Color}-{appName}-External-ALB",
                //        "External Application Load Balancer Security Group");
                //}
                //return;
                var loadBalancerArn = await loadBalancerHelper.CreateLoadBalancer(
                    $"{GlobalVariables.Enviroment.ToString()}-{appName}-ALB",
                    securirtyGroups.FindAll(o =>
                        o.DisplayName.Contains(appName) &&
                        o.DisplayName.Contains(GlobalVariables.Enviroment.ToString()) &&
                        o.Description == "Load Balancer Security Group").Select(o => o.GroupId).ToList(),
                    subnets.FindAll(o => o.DisplayName.Contains("Public")).Select(o => o.SubnetId).ToList());
                foreach (var color in colors)
                {
                    var targetGroup = sGroups.Find(o => o.Name.Contains($"{GlobalVariables.Enviroment}-{color}-{appName}"));
                    if (targetGroup == null)
                    {
                        LogServices.WriteLog(
                            $"{GlobalVariables.Enviroment}-{color}-{appName} target group doesn't exist",
                            LogType.Warn, "SwitchDeploymentService");
                        continue;
                    }
                    var tGroup = await loadBalancerHelper.CreateTargetGroup(
                        $"{GlobalVariables.Enviroment.ToString()}-{color}-{appName}-TG",
                        vpc.VpcId, "HTTP", 80);
                    await scalingGroupHelper.AttachLoadBalancerTargetGroups(
                        sGroups.Find(o => o.Name.Contains($"{GlobalVariables.Enviroment}-{color}-{appName}")),
                        new List<string>() { tGroup.TargetGroupArn });
                    tGroups.Add(tGroup);
                }
                var greenTargetGroup = tGroups.Find(o => o.TargetGroupName.Contains(Color.green.ToString()));
                if (greenTargetGroup != null)
                {
                    await loadBalancerHelper.CreatListener(loadBalancerArn, greenTargetGroup.TargetGroupArn,
                        "HTTP", PRODUCTION_HTTP_PORT);
                    await loadBalancerHelper.CreatListener(loadBalancerArn, greenTargetGroup.TargetGroupArn,
                        "HTTPS", PRODUCTION_HTTPS_PORT,
                        "arn:aws:acm:us-east-2:125237747044:certificate/abb607ce-4090-45cb-bf29-923e08106664");
                }
                var blueTargetGroup = tGroups.Find(o => o.TargetGroupName.Contains(Color.blue.ToString()));
                if (blueTargetGroup != null)
                {
                    await loadBalancerHelper.CreatListener(loadBalancerArn, blueTargetGroup.TargetGroupArn,
                        "HTTP", PRE_PRODUCTION_HTTP_PORT);
                    await loadBalancerHelper.CreatListener(loadBalancerArn, blueTargetGroup.TargetGroupArn,
                        "HTTPS", PRE_PRODUCTION_HTTPS_PORT,
                        "arn:aws:acm:us-east-2:125237747044:certificate/abb607ce-4090-45cb-bf29-923e08106664");
                }
            }
        }

        public async Task<List<ApplicationLoadBalancerModel>> GetApplicationLoadBalancers()
        {
            ALBHelper loadBalancerHelper = new ALBHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var applicationLoadBalancers = new List<ApplicationLoadBalancerModel>();
            var loadBalancers = await loadBalancerHelper.GetLoadBalancerList();
            foreach (var loadBalancer in loadBalancers)
            {
                var applicationLoadBalancer = new ApplicationLoadBalancerModel();
                applicationLoadBalancer.LoadBalancer = loadBalancer;
                applicationLoadBalancer.Listeners = await loadBalancerHelper.GetListenerList(loadBalancer.LoadBalancerArn);
                applicationLoadBalancer.Listeners = applicationLoadBalancer.Listeners.OrderBy(o => o.Port).ToList();
                applicationLoadBalancers.Add(applicationLoadBalancer);
            }
            return applicationLoadBalancers;
        }

        public async Task SwitchDeployment()
        {
            ALBHelper loadBalancerHelper = new ALBHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var loadBalancers = await loadBalancerHelper.GetLoadBalancerList();
            foreach (var loadBalancer in loadBalancers)
            {
                var listeners = await loadBalancerHelper.GetListenerList(loadBalancer.LoadBalancerArn);
                var prodHttpListener = listeners.Find(o => o.Port == PRODUCTION_HTTP_PORT);
                var prodHttpsListener = listeners.Find(o => o.Port == PRODUCTION_HTTPS_PORT);
                var preProdHttpListener = listeners.Find(o => o.Port == PRE_PRODUCTION_HTTP_PORT);
                var preProdHttpsListener = listeners.Find(o => o.Port == PRE_PRODUCTION_HTTPS_PORT);

                //Http listners (80, 8080) are not manditory
                if (prodHttpListener!= null && preProdHttpListener!=null)
                {
                    await loadBalancerHelper.ChangeListenerTargets(prodHttpListener.ListenerArn, preProdHttpListener.TargetArn);
                    await loadBalancerHelper.ChangeListenerTargets(preProdHttpListener.ListenerArn, prodHttpListener.TargetArn);
                }
                //Https listners (443, 4343) are manditory. If not created, should go back to rebuild the ALB.
                if (prodHttpsListener != null && preProdHttpsListener != null)
                {
                    await loadBalancerHelper.ChangeListenerTargets(prodHttpsListener.ListenerArn, preProdHttpsListener.TargetArn);
                    await loadBalancerHelper.ChangeListenerTargets(preProdHttpsListener.ListenerArn, prodHttpsListener.TargetArn);
                }
                else
                {
                    throw new Exception($"The Https listers for {loadBalancer.LoadBalancerName} are not created!");
                }
            }
        }

        public async Task ClearTargetGroupAttachments()
        {
            var loadBalancerHelper = new ALBHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var scalingGroupHelper = new AutoScalingHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var autoScalingGroups = await scalingGroupHelper.GetAutoScalingGroupList(true);
            foreach (var autoScalingGroup in autoScalingGroups)
            {
                await scalingGroupHelper.ClearAutoScalingGroupAttachedTargetGroups(autoScalingGroup);
            }
        }
    }
}
