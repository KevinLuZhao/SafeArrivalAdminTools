using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System.Collections.Generic;
using System.Linq;

namespace SafeArrival.AdminTools.DAL
{
    public class EnvironmentAccountDb
    {
        private DynamoDBHelper<EnvironmentAccount> helper;
        private string tableName = "sa_env_accounts";
        public EnvironmentAccountDb()
        {
            helper = new DynamoDBHelper<EnvironmentAccount>();
        }

        public EnvironmentAccount GetRoleByEnv(string env)
        {
            return helper.QueryItems(tableName, "Environment", ":v_Environment", env).First();
        }

        public Dictionary<string, EnvironmentAccount> GetEnvironmentAccounts()
        {
            var ret = new Dictionary<string, EnvironmentAccount>();
            var accounts = helper.ScanTable(tableName);
            foreach (var account in accounts)
            {
                if (account.Enabled)
                    ret.Add(account.Environment.ToString(), account);
            }
            return ret;
        }
    }
}
