using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_PeeringConnection
    {
        public string VpcPeeringConnectionId { get; set; }
        public string AccepterVpc { get; set; }
        public string RequesterVpc { get; set; }
        public string Status { get; set; }
    }
}
