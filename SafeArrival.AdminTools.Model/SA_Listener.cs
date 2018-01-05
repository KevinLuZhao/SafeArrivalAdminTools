using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_Listener
    {
        //public List<Certificate> Certificates { get; set; }
        public string Rule { get; set; }
        public string ListenerArn { get; set; }
        public string LoadBalancerArn { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; }
        public string SslPolicy { get; set; }
    }
}
