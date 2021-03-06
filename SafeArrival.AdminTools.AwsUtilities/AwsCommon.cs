﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.Model;
using Amazon.Runtime.CredentialManagement;
using System.Reflection;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class AwsCommon
    {
        public static Amazon.RegionEndpoint GetRetionEndpoint(string region)
        {
            switch (region)
            {
                case "us-east-1":
                    return Amazon.RegionEndpoint.USEast1;
                case "us-east-2":
                    return Amazon.RegionEndpoint.USEast2;
                case "us-west-1":
                    return Amazon.RegionEndpoint.USWest1;
                case "us-west-2":
                    return Amazon.RegionEndpoint.USWest2;
                case "ca-central-1":
                    return Amazon.RegionEndpoint.CACentral1;
                default:
                    return Amazon.RegionEndpoint.USEast2;
            }
        }

        //public static string GetAwsAccountByEnvironment(string env)
        //{
        //    var helper = new DynamoDBHelper<EnvironmentAccount>();
        //    helper.ScanTable("");
        //    switch (env)
        //    {
        //        case "staging:
        //            return "383514732995";
        //        case Environment.stagingca:
        //            return "189582403580";
        //        case Environment.production:
        //            return "683281545121";
        //        case Environment.productionca:
        //            return "944027855121";
        //        default:
        //            return "125237747044";
        //    }
        //}

        public static Dictionary<string, EnvironmentAccount> GetEnvironmentAccounts()
        {
            var helper = new DynamoDBHelper<EnvironmentAccount>();
            var ret = new Dictionary<string, EnvironmentAccount>();
            foreach (var account in helper.ScanTable("sa_env_accounts"))
            {
                if (account.Enabled)
                    ret.Add(account.Environment.ToString(), account);
            }
            return ret;
        }

        public static List<string> GetEnvironmentList()
        {
            return GetEnvironmentAccounts().Keys.OrderBy(o=>o).ToList();
        }
    }
}
