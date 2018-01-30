using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_PipelineSummary
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        
        public DateTime Updated { get; set; }

        public int Version { get; set; }
    }
}
