using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class ConfigurationOptionSettingModel
    {
        public string Namespace { get; set; }
        public string OptionName { get; set; }
        public string ResourceName { get; set; }
        public string Value { get; set; }
    }
}
