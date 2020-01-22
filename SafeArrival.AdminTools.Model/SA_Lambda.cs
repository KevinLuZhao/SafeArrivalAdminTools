using System;
using System.Collections.Generic;

namespace SafeArrival.AdminTools.Model
{
    public class SA_Lambda
    {
        public string FunctionName { get; set; }
        public string FunctionArn { get; set; }
        public DateTime LastModified { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }
}
