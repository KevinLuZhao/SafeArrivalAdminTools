using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.DAL;

namespace SafeArrival.AdminTools.BLL
{
    public class SystemManagement
    {
        //public async Task ShutDownSystem(Environment environment)
        //{
        //    await 
        //}
        AutoScaleGroupSettingsDb db = new AutoScaleGroupSettingsDb();

        public async Task StartSystem(Model.Environment profile, string region, List<AutoScalingGroupSettings> lstSettings)
        {
            try
            {
                ElasticBeanstalkServices service = new ElasticBeanstalkServices(GlobalVariables.Enviroment, GlobalVariables.Region);
                service.DisableScheduleActions(false);
                //Start RDS
                RDSHelper rdsHelper = new RDSHelper(profile, region);
                try
                {
                    var response = await rdsHelper.GetRDSInstance();
                    await rdsHelper.StartRdsInstance(response.DBInstanceIdentifier);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().Name == "InvalidDBInstanceStateException")
                    {
                        LogServices.WriteLog(
                            $"RDS instance is not stopped, system starting will continue. Detailed message: {ex.Message}", 
                            LogType.Warn, GlobalVariables.Enviroment.ToString());
                    }
                }
                //Start applications by changing the auto scaling group
                var autoScalingHelper = new AutoScalingHelper(profile, region);
                var lstGroup = await autoScalingHelper.GetAutoScalingGroupList();
                foreach (var group in lstGroup)
                {
                    var settings = lstSettings.Find(o => group.Name.IndexOf(o.Environment.ToString()) == 0 &&
                    group.Name.IndexOf(o.Application.ToString()) > 0);
                    if (settings != null)
                        await autoScalingHelper.ChangeScalingGroup(group.AutoScalingGroupName, settings);
                }
            }
            catch (Exception ex)
            {
                LogServices.WriteLog(ex.Message + " Stack trace: " + ex.StackTrace, LogType.Error, GlobalVariables.Enviroment.ToString());
            }
        }
        public async Task ShutDownSystem(Model.Environment profile, string region)
        {
            var autoScalingHelper = new AutoScalingHelper(profile, region);
            var lstGroup = await autoScalingHelper.GetAutoScalingGroupList();
            foreach (var group in lstGroup)
            {
                await autoScalingHelper.StopScalingGroup(group.AutoScalingGroupName);
            }

            ElasticBeanstalkServices service = new ElasticBeanstalkServices(GlobalVariables.Enviroment, GlobalVariables.Region);
            service.DisableScheduleActions(true);

            //helper.ShutdownSystem();
            RDSHelper rdsHelper = new RDSHelper(profile, region);
            var response = await rdsHelper.GetRDSInstance();
            await rdsHelper.StopRdsInstance(response.DBInstanceIdentifier);
        }

        public void SaveAllAutoScalingGroupSettings(List<AutoScalingGroupSettings> settings)
        {
            foreach (var setting in settings)
            {
                db.Add(setting);
            }
        }

        public void InitAutoScalingGroupSettings(Model.Environment env)
        {
            AutoScaleGroupSettingsDb db = new AutoScaleGroupSettingsDb();
            foreach (var application in Enum.GetNames(typeof(ApplicationServer)))
            {
                AutoScalingGroupSettings settings = new AutoScalingGroupSettings()
                {
                    Application = (ApplicationServer)Enum.Parse(typeof(ApplicationServer), application),
                    Environment = env,
                    MaxSize = 1,
                    MinSize = 1,
                    DesiredCapacity = 1
                };
                db.Add(settings);
            }
        }

        public List<AutoScalingGroupSettings> GetAutoScalingGroupSettingsByEnv(Model.Environment env)
        {
            AutoScaleGroupSettingsDb db = new AutoScaleGroupSettingsDb();
            return db.GetSettingsByEnv(env);
        }

        public async Task<List<ScheduledAction>> GetApiScheduledActions()
        {
            ElasticBeanstalkServices service = new ElasticBeanstalkServices(GlobalVariables.Enviroment, GlobalVariables.Region);
            return await service.GetScheduledActions();
        }
    }
}
