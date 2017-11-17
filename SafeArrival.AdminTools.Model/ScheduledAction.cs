using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class ScheduledAction
    {
        public string Name { get; set; }
        public string DesiredCapacity { get; set; }
        public string MaxSize { get; set; }
        public string MinSize { get; set; }
        public string Recurrence { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Suspend { get; set; }
    }
}
