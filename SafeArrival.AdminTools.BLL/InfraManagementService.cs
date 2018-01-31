using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;


namespace SafeArrival.AdminTools.BLL
{
    public class InfraManagementService
    {
        public async Task<string> DeleteStack(string stackName, AsyncCallback callBack)
        {
            var helper = new CloudFormationHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            return await helper.DeleteStack(stackName, callBack);
        }

        public async Task<List<SA_Stack>> GetStackList()
        {
            var helper = new CloudFormationHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            return await helper.GetStackList();
        }

        public async Task<List<SA_PipelineSummary>> GetCodePipelinList()
        {
            var helper = new CodePipelineHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            return await helper.GetCodePipelineList();
        }

        public async Task BuildCodePipelinelevel_1And2(int level, string oAuthToken)
        {
            var helper = new CloudFormationHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);

            var sa_parameters = new List<KeyValuePair<string, string>>();
            sa_parameters.Add(new KeyValuePair<string, string>("Environment", GlobalVariables.Enviroment.ToString()));
            sa_parameters.Add(new KeyValuePair<string, string>("OAuthToken", oAuthToken));

            string stackName = $"{GlobalVariables.Enviroment}-level-{level}";
            if (level == 2)
            {
                sa_parameters.Add(new KeyValuePair<string, string>("Color", GlobalVariables.Color));
                stackName += $"-{GlobalVariables.Color}";
            }

            await helper.CreateStack
            (
                ConfigurationManager.AppSettings["InfraFileFolder"],
                stackName,
                ReadCodePipelineConfigs(level),
                sa_parameters
            );
            LogServices.WriteLog($"{GlobalVariables.Enviroment}-level-{level}-{GlobalVariables.Color} is created.",
                    LogType.Information, GlobalVariables.Enviroment.ToString());
        }

        public async Task BuildCodePipelinelevel_3(List<string> apps)
        {
            var helper = new CloudFormationHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);

            foreach (string app in apps)
            {
                var sa_parameters = new List<KeyValuePair<string, string>>();
                sa_parameters.Add(new KeyValuePair<string, string>("Environment", GlobalVariables.Enviroment.ToString()));
                sa_parameters.Add(new KeyValuePair<string, string>("Color", GlobalVariables.Color));
                sa_parameters.Add(new KeyValuePair<string, string>("Application", app));

                await helper.CreateStack
                (
                    ConfigurationManager.AppSettings["InfraFileFolder"],
                    $"{GlobalVariables.Enviroment}-level-3-{app}-{GlobalVariables.Color}",
                    ReadCodePipelineConfigs(3),
                    sa_parameters
                );
                LogServices.WriteLog($"{GlobalVariables.Enviroment}-level-3-{GlobalVariables.Color}-{app} is created.",
                    LogType.Information, GlobalVariables.Enviroment.ToString());
            }
        }

        public async Task SetSisEventTrigger()
        {
            var s3Helper = new S3Helper(
               GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color, 
               $"safe-arrival-{GlobalVariables.Region}-{GlobalVariables.Enviroment}-sisbucket");
            var lambdaHelper = new LambdaHelper(
               GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var db = new EnvironmentAccountDb();
            var account = db.GetRoleByEnv(GlobalVariables.Enviroment);
            await s3Helper.PutNotification();
            await lambdaHelper.AddPolicy(account.Account);
            await Task.Delay(1000);
            await s3Helper.PutNotification
            (
                $"sis-raw-event-{GlobalVariables.Enviroment.ToString()}",
                $"arn:aws:lambda:{GlobalVariables.Region}:{account.Account}:function:SafeArrival-SIS-{GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                "s3:ObjectCreated:Put"
            );
        }

        //No color for now.
        public async Task SetDNS()
        {
            var helper = new CloudFormationHelper(
               GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);

            var sa_parameters = new List<KeyValuePair<string, string>>();
            sa_parameters.Add(new KeyValuePair<string, string>("Environment", GlobalVariables.Enviroment.ToString()));
            sa_parameters.Add(new KeyValuePair<string, string>("Color", GlobalVariables.Color));
            string stackName = $"{GlobalVariables.Enviroment}-DNS";

            await helper.CreateStack
            (
                ConfigurationManager.AppSettings["InfraFileFolder"],
                stackName,
                ReadCodePipelineConfigs(4),
                sa_parameters
            );
        }

        private List<KeyValuePair<string, string>> GetCodePipelingParameters(int level)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            switch (level)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    throw new Exception($"{level} is an invalid infrastructure level");
            }
            return parameters;
        }

        private string ReadCodePipelineConfigs(int level)
        {
            var filePath = Path.Combine(ConfigurationManager.AppSettings["InfraFileFolder"], "master", $"Level-{level}.yaml");
            if (level == 4)
            {
                filePath = Path.Combine(ConfigurationManager.AppSettings["InfraFileFolder"], "cloudformation", "DNS.yaml");
            }
            if (!File.Exists(filePath))
            {
                throw new Exception($"Can't find file {filePath}!");
            }
            return File.ReadAllText(filePath);
        }
    }
}
