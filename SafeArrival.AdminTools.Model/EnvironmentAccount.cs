using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class EnvironmentAccount
    {
        public string Environment { get; set; }
        public string DNS { get; set; }
        public string Account { get; set; }
        public string Region { get; set; }
        public string RoleArn { get; set; }
        public string TCBuildConfig { get; set; }
    }
}
