using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
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

        public async Task BuildCodePipelinelevel1(int level, string oAuthToken)
        {
            var helper = new CloudFormationHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var filePath = Path.Combine(ConfigurationManager.AppSettings["InfraFileFolder"], "master", $"Level-{level}.yaml");
            if (!File.Exists(filePath))
            {
                return;
            }
            var content = File.ReadAllText(filePath);
            var sa_parameters = new List<KeyValuePair<string, string>>();
            sa_parameters.Add(new KeyValuePair<string, string>("OAuthToken", oAuthToken));
            sa_parameters.Add(new KeyValuePair<string, string>("Environment", GlobalVariables.Enviroment.ToString()));
            await helper.CreateStack
            (
            ConfigurationManager.AppSettings["InfraFileFolder"],
            $"{GlobalVariables.Enviroment}-level-{level}",
            content,
            sa_parameters
            );
        }

        private List<KeyValuePair<string,string>> GetCodePipelingParameters(int level)
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
    }
}
