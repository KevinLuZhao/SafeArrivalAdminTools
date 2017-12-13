using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class DynamodbScanCondition
    {
        public string AttributeName { get; set; }
        //public DynamodbScanOperator Operator { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }
}
