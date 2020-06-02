using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SafeArrival.AdminTools.Model;
using System.Configuration;
using LibGit2Sharp;

namespace SafeArrival.AdminTools.BLL
{
    public class UtilsGitManager
    {
        public bool CheckGitRepository(out string message)
        {
            message = null;
            //string githubTokenPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "gittoken");
            string githubTokenPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), ".git-credentials");
            if (!File.Exists(githubTokenPath))
            {
                message = $"Please set value to {githubTokenPath}.";
                return false;
            }

            bool ret;
            var gitBranch = GetLocalRepositoryBranch();
            switch (gitBranch)
            {
                case "development":
                    ret = (GlobalVariables.Enviroment == "development" || GlobalVariables.Enviroment == "infra");
                    break;
                case "staging":
                    ret = (GlobalVariables.Enviroment == "staging" || GlobalVariables.Enviroment == "stagingca" || GlobalVariables.Enviroment == "fake-prod");
                    break;
                case "production":
                    ret = (GlobalVariables.Enviroment == "production" || GlobalVariables.Enviroment == "productionca");
                    break;
                default:
                    ret = false;
                    message = $"Git branch {gitBranch} is illegal";
                    break;
            }
            //var ret = (GlobalVariables.Enviroment.ToString() == gitBranch);
            if (!ret)
                message = $"Current target environment is '{GlobalVariables.Enviroment.ToString().ToUpper()}', while the Github local branch is '{gitBranch.ToUpper()}'.";
            return ret;
        }

        //At present only consider infra repository. Will decide if the parameter is necessary.
        public string GetLocalRepositoryBranch()
        {
            string path = ConfigurationManager.AppSettings["InfraFileFolder"];
            if (LibGit2Sharp.Repository.IsValid(path))
            {
                var repo = new Repository(path);
                return repo.Head.FriendlyName;
            }
            else
            {
                return $"No repository is found from the path of {path}, please check your config file";
            }
        }
    }
}
