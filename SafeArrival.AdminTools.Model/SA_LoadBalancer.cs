using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_LoadBalancer
    {
        //public List<AvailabilityZone> AvailabilityZones { get; set; }
        public string CanonicalHostedZoneId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string DNSName { get; set; }
        public string LoadBalancerArn { get; set; }
        public string LoadBalancerName { get; set; }
        public string Scheme { get; set; }
        //public List<string> SecurityGroups { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string VpcId { get; set; }
    }
}
