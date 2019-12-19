using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_Ec2Instance
    {
        public string Name { get; set; }
        public string InstanceId { get; set; }
        public string InstanceType { get; set; }
        public string VpcId { get; set; }
        public string State { get; set; }
    }
}
