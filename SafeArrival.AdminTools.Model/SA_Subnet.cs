using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_Subnet
    {
        public string DisplayName { get; set; }
        public bool AssignIpv6AddressOnCreation { get; set; }
        public string AvailabilityZone { get; set; }
        public int AvailableIpAddressCount { get; set; }
        public string CidrBlock { get; set; }
        public bool DefaultForAz { get; set; }
        //public List<SubnetIpv6CidrBlockAssociation> Ipv6CidrBlockAssociationSet { get; set; }
        public bool MapPublicIpOnLaunch { get; set; }
        public string State { get; set; }
        public string SubnetId { get; set; }
        //public List<Tag> Tags { get; set; }
        public string VpcId { get; set; }
    }
}
