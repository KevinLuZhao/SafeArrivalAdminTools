using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class CredentiaslManager
    {
        public static Dictionary<string, AssumeRoleAWSCredentials> GlobalRoleCredentialsCache { get; set; }
        public static Dictionary<string, string> GlobalRoleArnCache { get; set; }
        public static AssumeRoleAWSCredentials GetCredential(string env)
        {
            if (GlobalRoleCredentialsCache == null)
            {
                GlobalRoleCredentialsCache = new Dictionary<string, AssumeRoleAWSCredentials>();
                foreach (var strEnv in AwsCommon.GetEnvironmentList())
                {
                    GlobalRoleCredentialsCache.Add(strEnv, null);
                }
            }
            if (GlobalRoleArnCache == null)
            {
                GlobalRoleArnCache = new Dictionary<string, string>();
                var client = new AmazonDynamoDBClient(GetDynamoDbCredential(), AwsCommon.GetRetionEndpoint("us-east-2"));
                var request = new ScanRequest("sa_env_accounts");
                var response = client.Scan(request);
                foreach (var item in response.Items)
                {
                    GlobalRoleArnCache.Add(item["Environment"].S, item["RoleArn"].S + "/" + ConfigurationSettings.AppSettings["Role"]);
                }
            }

            if (GlobalRoleCredentialsCache[env.ToString()] == null)
            {
                GlobalRoleCredentialsCache[env.ToString()] = 
                    new AssumeRoleAWSCredentials(
                        new StoredProfileAWSCredentials(), 
                        GlobalRoleArnCache[env.ToString()], 
                        "AssumeRole");
            }

            return GlobalRoleCredentialsCache[env.ToString()];
        }

        public static StoredProfileAWSCredentials GetDynamoDbCredential()
        {
            return new StoredProfileAWSCredentials("development");
        }
    }
}
