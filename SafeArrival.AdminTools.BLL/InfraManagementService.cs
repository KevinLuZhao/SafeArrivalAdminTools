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

        public async Task BuildCodePipelinelevel(int level, string oAuthToken)
        {
            var helper = new CloudFormationHelper(
                GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var filePath = Path.Combine(ConfigurationManager.AppSettings["InfraFileFolder"], "master", $"Level-{level}.yaml");
            if (!File.Exists(filePath))
            {
                return;
            }
            var content = File.ReadAllText(filePath);
            await helper.CreateStack
            (
                ConfigurationManager.AppSettings["InfraFileFolder"],
                $"{GlobalVariables.Enviroment}-level-{level}",
                content,
                oAuthToken
            );
        }
    }
}
