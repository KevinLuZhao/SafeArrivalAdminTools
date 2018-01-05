using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_TargetGroup
    {
        //public int HealthCheckIntervalSeconds { get; set; }
        //public string HealthCheckPath { get; set; }
        //public string HealthCheckPort { get; set; }
        //public ProtocolEnum HealthCheckProtocol { get; set; }
        //public int HealthCheckTimeoutSeconds { get; set; }
        //public int HealthyThresholdCount { get; set; }
        public List<string> LoadBalancerArns { get; set; }
        //public Matcher Matcher { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; }
        public string TargetGroupArn { get; set; }
        public string TargetGroupName { get; set; }
        //public TargetTypeEnum TargetType { get; set; }
        //public int UnhealthyThresholdCount { get; set; }
        public string VpcId { get; set; }
    }
}
