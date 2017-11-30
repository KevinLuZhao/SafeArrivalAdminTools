using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System.Reflection;

namespace SafeArrival.AdminTools.BLL
{
    public class ElasticBeanstalkServices
    {
        ElasticBeanstalkHelper helper;

        public ElasticBeanstalkServices(Model.Environment profile, string region)
        {
            helper = new AwsUtilities.ElasticBeanstalkHelper(profile, region);
        }

        public void CleanScheduledActionConfig()
        {
            helper.ChangeScheduleActionInstnaceNum("ScaleUpAction1", 0, 0, 0);
            helper.ChangeScheduleActionInstnaceNum("ScaleDownAction1", 0, 0, 0);
        }

        public void ResetScheduledActionConfig()
        {
            helper.ChangeScheduleActionInstnaceNum("ScaleUpAction1", 2, 6, 1);
            helper.ChangeScheduleActionInstnaceNum("ScaleDownAction1", 1, 6, 1);
        }

        public void DisableScheduleActions(bool command)
        {
            helper.DisableScheduleActions(command);
        }

        public async Task<List<ScheduledAction>> GetScheduledActions()
        {
            var ret = new List<ScheduledAction>();
            var lstSettingItems = await helper.GetScheduledActionSettingItems();
            HashSet<string> actionNames = new HashSet<string>();
            foreach (var setting in lstSettingItems)
            {
                actionNames.Add(setting.ResourceName);
            }
            foreach (string actionName in actionNames)
            {
                ScheduledAction action = new ScheduledAction();
                action.Name = actionName;
                foreach (var prop in typeof(ScheduledAction).GetProperties())
                {
                    var valueItem = lstSettingItems.Find
                        (o => o.OptionName == prop.Name && o.ResourceName == actionName);
                    if (valueItem!= null)
                    prop.SetValue(action, valueItem.Value);
                }
                ret.Add(action);
            }
            
            return ret;
        }
    }
}
