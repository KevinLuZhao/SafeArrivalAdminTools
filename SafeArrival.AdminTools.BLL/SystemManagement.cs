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
        AutoScaleGroupSettingsDb db = new AutoScaleGroupSettingsDb();

        public async Task StartSystem( 
            List<AutoScalingGroupSettings> lstSettings, bool isRdsMultyAz)
        {
            try
            {
                ElasticBeanstalkServices service = new ElasticBeanstalkServices();
                service.DisableScheduleActions(false);
                //Start RDS
                RDSHelper rdsHelper = new RDSHelper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
                try
                {
                    var response = await rdsHelper.GetRDSInstance();                  
                    await rdsHelper.StartRdsInstance(response.DBInstanceIdentifier, isRdsMultyAz);
                    LogServices.WriteLog($"{GlobalVariables.Enviroment} was started.", LogType.Information, GlobalVariables.Enviroment.ToString());
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
                var autoScalingHelper = new AutoScalingHelper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
                var lstGroup = await autoScalingHelper.GetEnvironmentAutoScalingGroupList();
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

        public async Task ShutDownSystem(bool stopJumpBox)
        {
            var autoScalingHelper = new AutoScalingHelper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var lstGroup = await autoScalingHelper.GetEnvironmentAutoScalingGroupList();
            if (!stopJumpBox)
            {
                var jumpBox = lstGroup.Find(o => o.Name.IndexOf("Jump") >= 0);
                if (jumpBox != null)
                    lstGroup.Remove(jumpBox);
            }
            foreach (var group in lstGroup)
            {
                await autoScalingHelper.StopScalingGroup(group.AutoScalingGroupName);
            }

            ElasticBeanstalkServices service = new ElasticBeanstalkServices();
            service.DisableScheduleActions(true);

            //helper.ShutdownSystem();
            RDSHelper rdsHelper = new RDSHelper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            var response = await rdsHelper.GetRDSInstance();
            await rdsHelper.StopRdsInstance(response.DBInstanceIdentifier);
            LogServices.WriteLog($"{GlobalVariables.Enviroment} was shut down.", LogType.Information, GlobalVariables.Enviroment.ToString());
        }

        public async Task StartRdsInstances(List<SA_RdsInstance> lstInstances)
        {
            RDSHelper rdsHelper = new RDSHelper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            foreach (var instance in lstInstances)
            {
                await rdsHelper.StartRdsInstance(instance.DBInstanceIdentifier, instance.MultiAZ);
                LogServices.WriteLog($"{GlobalVariables.Enviroment} RDS {instance.DBInstanceIdentifier} was started.", LogType.Information, GlobalVariables.Enviroment.ToString());
            }
        }

        public async Task StopRdsInstances(List<SA_RdsInstance> lstInstances)
        {
            RDSHelper rdsHelper = new RDSHelper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
            foreach (var instance in lstInstances)
            {
                await rdsHelper.StopRdsInstance(instance.DBInstanceIdentifier);
                LogServices.WriteLog($"{GlobalVariables.Enviroment} RDS {instance.DBInstanceIdentifier} was shut down.", LogType.Information, GlobalVariables.Enviroment.ToString());
            }
        }

        public void SaveAllAutoScalingGroupSettings(List<AutoScalingGroupSettings> settings)
        {
            foreach (var setting in settings)
            {
                db.Add(setting);
            }
            LogServices.WriteLog($"{GlobalVariables.Enviroment} auto scaling group settings were started.", LogType.Information, GlobalVariables.Enviroment.ToString());
        }

        public void InitAutoScalingGroupSettings(string env)
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

        public List<AutoScalingGroupSettings> GetAutoScalingGroupSettingsByEnv(string env)
        {
            AutoScaleGroupSettingsDb db = new AutoScaleGroupSettingsDb();
            return db.GetSettingsByEnv(GlobalVariables.Enviroment);
        }

        public async Task<List<ScheduledAction>> GetApiScheduledActions()
        {
            ElasticBeanstalkServices service = new ElasticBeanstalkServices();
            return await service.GetScheduledActions();
        }
    }
}
