using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class DeployDnsModel
    {
        public string Application { get; set; }
        //The endpoint to working load balancer url
        public string LiveEndpoint { get; set; }
        //The endpoint to S3 bucket static websit hosting url 
        public string MaintenanceEndpoint { get; set; }
    }
}
