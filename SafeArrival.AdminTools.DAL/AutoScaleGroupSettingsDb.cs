using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.AwsUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.DAL
{
    public class AutoScaleGroupSettingsDb
    {
        private DynamoDBHelper<AutoScalingGroupSettings> helper;
        private string tableName = "sa_auto_scaling_group_settings";
        public AutoScaleGroupSettingsDb()
        {
            helper = new DynamoDBHelper<AutoScalingGroupSettings>();
        }
        public void Add(AutoScalingGroupSettings obj)
        {
            helper.CreateItem(tableName, obj);
        }

        public List<AutoScalingGroupSettings> GetSettingsByEnv(Model.Environment env)
        {
            return helper.QueryItems(tableName, "Environment", ":v_Environment", env.ToString());
        }
    }
}
