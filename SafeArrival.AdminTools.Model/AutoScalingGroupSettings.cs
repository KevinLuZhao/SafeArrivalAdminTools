using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class AutoScalingGroupSettings
    {
        public string Environment { get; set; }
        public ApplicationServer Application { get; set; }
        public int MaxSize { get; set; }
        public int MinSize { get; set; }
        public int DesiredCapacity { get; set; }
        public string HealthCheckType { get; set; }
        public int HealthCheckGracePeriod { get; set; }
        //public string DisplayName { get; set; }
    }
}
