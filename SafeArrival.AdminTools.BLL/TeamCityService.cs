using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naos.TeamCity.APIWrapper;
using Naos.TeamCity.APIWrapper.Types;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.BLL
{
    public class TeamCityService
    {
        Client client;
        public TeamCityService()
        {
            var tcCredential = Utils.GetTeamcityCredential();
            client = new Client(tcCredential.HostName, tcCredential.UserName, tcCredential.Password);
        }
        public void RunBuildConfig(string buildConfigureId)
        {
            //var config = client.GetBuildConfigByConfigurationId(buildConfigureId);

            client.TriggerBuild(buildConfigureId);
        }

        public List<TeamCityProject> ListProjects()
        {
            try
            {
                var saTC_Projects = new List<TeamCityProject>();
                var tcProjects = client.GetAllProjects().ToList().FindAll(o => o.id.Contains("AwsCompleteDeploy_"));
                foreach(var tcProject in tcProjects)
                {
                    saTC_Projects.Add(new TeamCityProject() { href = tcProject.href, id = tcProject.id, name = tcProject.name });
                }
                return saTC_Projects;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<string> ListConfigruations()
        {
            try
            {
                //var x = client.GetBuildConfigByConfigurationId("AwsCompleteDeploy_Product_BuildAwsDeploymentZipFilesConfig");
                var saTC_BuildCibfugs = new List<string>();
                var tcBuildConfigs = client.GetAllBuildConfigs().ToList().
                    FindAll(o => o.id.Contains("AwsCompleteDeploy_") && o.id.Contains("BuildAwsDeploymentZipFiles"));
                foreach (var tcBuildConfig in tcBuildConfigs)
                {
                    saTC_BuildCibfugs.Add(tcBuildConfig.id);
                }
                return saTC_BuildCibfugs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public List<string> GetBuildConfig(string buildConfigId)
        //{
        //    try
        //    {
        //        //var x = client.GetBuildConfigByConfigurationId("AwsCompleteDeploy_Product_BuildAwsDeploymentZipFilesConfig");
        //        var saTC_BuildCibfugs = new List<string>();
        //        var tcBuildConfigs = client.GetAllBuildConfigs().ToList().
        //            FindAll(o => o.id.Contains("AwsCompleteDeploy_") && o.id.Contains("BuildAwsDeploymentZipFiles"));
        //        foreach (var tcBuildConfig in tcBuildConfigs)
        //        {
        //            saTC_BuildCibfugs.Add(tcBuildConfig.id);
        //        }
        //        return saTC_BuildCibfugs;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
