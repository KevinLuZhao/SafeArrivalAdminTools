using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SafeArrival.AdminTools.Model;
using System.Xml;
using System.Configuration;
using LibGit2Sharp;

namespace SafeArrival.AdminTools.BLL
{
    public class Utils
    {
        static public string FormatresourceName(string resourTypeName)
        {
            return $"{GlobalVariables.Enviroment.ToString()}_{resourTypeName}";
        }

        static public string GetGitToken()
        {
            var doc = GetCredentialsFile();
            return doc.SelectSingleNode("github").Value;
        }

        static public TeamCityCredencial GetTeamcityCredential()
        {
            string xPathRoot = "SafeArrivalCredentials/teamcity";
            var doc = GetCredentialsFile();
            var tcNode = doc.SelectSingleNode(xPathRoot);
            var credential = new TeamCityCredencial()
            {
                HostName = tcNode.SelectSingleNode("host_name").InnerText,
                UserName = tcNode.SelectSingleNode("user_name").InnerText,
                Password = tcNode.SelectSingleNode("password").InnerText,
            };
            return credential;
        }

        static private XmlDocument GetCredentialsFile()
        {
            string path = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "safearrivalcredentials.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }
    }
}
