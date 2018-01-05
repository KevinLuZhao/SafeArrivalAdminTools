using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class ApplicationLoadBalancerModel
    {
        public SA_LoadBalancer LoadBalancer { get; set; }
        public List<SA_Listener> Listeners { get; set; }
    }
}
