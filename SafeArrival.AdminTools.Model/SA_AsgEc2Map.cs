using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_AsgEc2Map
    {
        public string AutoScalingGroupName { get; set; }
        public string AsgName { get; set; }
        public string InstanceId { get; set; }
        //public string InstanceName { get; set; }
    }
}
