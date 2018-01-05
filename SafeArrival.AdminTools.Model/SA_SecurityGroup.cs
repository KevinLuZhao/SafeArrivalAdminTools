using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_SecurityGroup
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        //public List<IpPermission> IpPermissions { get; set; }
        //public List<IpPermission> IpPermissionsEgress { get; set; }
        public string OwnerId { get; set; }
        //public List<Tag> Tags { get; set; }
        public string VpcId { get; set; }
    }
}
