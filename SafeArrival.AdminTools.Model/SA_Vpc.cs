using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_Vpc
    {
        public string VpcId { get; set; }
        public string Name { get; set; }
        public string CidrBlock { get; set; }
        public string State { get; set; }
    }
}
