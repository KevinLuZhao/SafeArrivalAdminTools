using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public EnvironmentAccount GetRoleByEnv(Model.Environment env)
        {
            return helper.QueryItems(tableName, "Environment", ":v_Environment", env.ToString()).First();
        }

        public Dictionary<string, EnvironmentAccount> GetEnvironmentAccounts()
        {
            var ret = new Dictionary<string, EnvironmentAccount>();
            foreach (var account in helper.ScanTable(tableName))
            {
                ret.Add(account.Environment.ToString(), account);
            }
            return ret;
        }
    }
}
