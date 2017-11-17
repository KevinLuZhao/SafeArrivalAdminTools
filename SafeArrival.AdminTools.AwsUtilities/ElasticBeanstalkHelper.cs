using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.ElasticBeanstalk;
using Amazon.ElasticBeanstalk.Model;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class ElasticBeanstalkHelper
    {
        public SafeArrival.AdminTools.Model.Environment Environment { get; }
        private AmazonElasticBeanstalkClient client;

        public ElasticBeanstalkHelper(Model.Environment profile, string region)
        {
            //Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials(profile.ToString());
            this.Environment = profile;
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonElasticBeanstalkClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public void ChangeConfig()
        {
            var requestSettings = new DescribeConfigurationSettingsRequest()
            {
                ApplicationName = $"Safe-Arrival-{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                EnvironmentName = $"{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}-API",
            };
            var response = client.DescribeConfigurationSettings(requestSettings);

            HashSet<string> setNameSpace = new HashSet<string>();
            HashSet<string> setOptionName = new HashSet<string>();
            foreach (var option in response.ConfigurationSettings[0].OptionSettings)
            {
                setNameSpace.Add(option.Namespace);
                setOptionName.Add(option.Namespace + "." + option.OptionName + "=" + option.Value);
            }
        }

        public void DisableScheduleActions(bool command)
        {
            var requestSettings = new DescribeConfigurationSettingsRequest()
            {
                ApplicationName = $"Safe-Arrival-{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                EnvironmentName = $"{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}-API",
            };
            var response = client.DescribeConfigurationSettings(requestSettings);
            
            var suspendSettings = response.ConfigurationSettings[0].OptionSettings.FindAll(
                o => o.Namespace == "aws:autoscaling:scheduledaction" && o.OptionName == "Suspend");
            foreach (var setting in suspendSettings)
            {
                setting.Value = command.ToString();
            }
            var request = new UpdateEnvironmentRequest()
            {
                ApplicationName = $"Safe-Arrival-{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                EnvironmentName = $"{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}-API",
                OptionSettings = suspendSettings
            };
            var responseUpdate = client.UpdateEnvironment(request);
        }

        public void ChangeScheduleActionInstnaceNum(string scheduleActionname, int desiredCapacity, int max, int min)
        {
            var requestSettings = new DescribeConfigurationSettingsRequest()
            {
                ApplicationName = $"Safe-Arrival-{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                EnvironmentName = $"{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}-API",
            };
            var response = client.DescribeConfigurationSettings(requestSettings);


            var settings = new List<ConfigurationOptionSetting>();
            var sourceSettings = response.ConfigurationSettings[0].OptionSettings.FindAll(
                o => o.Namespace == "aws:autoscaling:scheduledaction");
            //Now have to hard code the resource name because no way to store the setting original value before set them to 0;
            //HashSet<string> resourceNameList = new HashSet<string>();
            //foreach (var optionSettings in sourceSettings)
            //{
            //    resourceNameList.Add(optionSettings.ResourceName);
            //}
            foreach (var optionSettings in sourceSettings)
            {
                    if (optionSettings.OptionName == "DesiredCapacity")
                    {
                        settings.Add(new ConfigurationOptionSetting()
                        {
                            Namespace = optionSettings.Namespace,
                            OptionName = optionSettings.OptionName,
                            ResourceName = optionSettings.ResourceName,
                            Value = desiredCapacity.ToString()
                        });
                    }
                    if (optionSettings.OptionName == "MaxSize")
                    {
                        settings.Add(new ConfigurationOptionSetting()
                        {
                            Namespace = optionSettings.Namespace,
                            OptionName = optionSettings.OptionName,
                            ResourceName = optionSettings.ResourceName,
                            Value = max.ToString()
                        });
                    }
                if (optionSettings.OptionName == "MinSize")
                {
                    settings.Add(new ConfigurationOptionSetting()
                    {
                        Namespace = optionSettings.Namespace,
                        OptionName = optionSettings.OptionName,
                        ResourceName = optionSettings.ResourceName,
                        Value = min.ToString()
                    });
                }
            }
            //UpdateConfigurationTemplateRequest request = new UpdateConfigurationTemplateRequest()
            //{

            //};
            //client.UpdateConfigurationTemplate(request);
            var request = new UpdateEnvironmentRequest()
            {
                ApplicationName = $"Safe-Arrival-{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                EnvironmentName = $"{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}-API",
                OptionSettings = settings
            };
            client.UpdateEnvironment(request);
        }

        public List<ConfigurationOptionSettingModel> GetScheduledActionSettingItems()
        {
            var ret = new List<ConfigurationOptionSettingModel>();
            var requestSettings = new DescribeConfigurationSettingsRequest()
            {
                ApplicationName = $"Safe-Arrival-{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}",
                EnvironmentName = $"{ GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}-API",
            };
            var response = client.DescribeConfigurationSettings(requestSettings);

            var sourceSettings = response.ConfigurationSettings[0].OptionSettings.FindAll(
                o => o.Namespace == "aws:autoscaling:scheduledaction");

            foreach (var settings in sourceSettings)
            {
                ret.Add(ModelTransformer<ConfigurationOptionSetting, ConfigurationOptionSettingModel>.
                    TransformAwsModelToSafeArrivalModel(settings));
            }
            return ret;
        }
    }
}
