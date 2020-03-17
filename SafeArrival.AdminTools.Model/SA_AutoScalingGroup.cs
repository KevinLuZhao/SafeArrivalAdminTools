using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_AutoScalingGroup
    {
        public string AutoScalingGroupName { get; set; }
        public string AutoScalingGroupARN { get; set; }
        public string Name { get; set; }
        //public string Status { get; set; }
        public int RunningInstances { get; set; }

        public int MaxSize { get; set; }
        public int MinSize { get; set; }
        public int DesiredCapacity { get; set; }
        public DateTime CreatedTime { get; set; }

        public string HealthCheckType { get; set; }
        public int HealthCheckGracePeriod { get; set; }
    }
}
