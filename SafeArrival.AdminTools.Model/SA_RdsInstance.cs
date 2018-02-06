using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_RdsInstance
    {
        public string DBInstanceIdentifier { get; set; }
        public string DBInstanceArn { get; set; }
        public string RdsEnvinronment { get; set; }
        public string Status { get; set; }
        public bool MultiAZ { get; set; }
    }
}
