//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class AwsHelperBase
    {
        protected Environment environment;
        protected string region;
        protected string color;
        public AwsHelperBase(Environment profile, string region, string color)
        {
            environment = profile;
            this.region = region;
            this.color = color;
        }
    }
}
