using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class DynamodbScanOperator
    {
        //readonly DynamodbScanOperator
        //
        // Summary:
        //     Constant BEGINS_WITH for DynamodbScanOperator
        public static string BEGINS_WITH;
        //
        // Summary:
        //     Constant BETWEEN for DynamodbScanOperator
        public static string BETWEEN { get { return "BETWEEN"; } }
        //
        // Summary:
        //     Constant CONTAINS for DynamodbScanOperator
        public static string CONTAINS { get { return "CONTAINS"; } }
        //
        // Summary:
        //     Constant EQ for DynamodbScanOperator
        public static string EQ { get { return "EQ"; } }
        //
        // Summary:
        //     Constant GE for DynamodbScanOperator
        public static string GE;
        //
        // Summary:
        //     Constant GT for DynamodbScanOperator
        public static string GT;
        //
        // Summary:
        //     Constant IN for DynamodbScanOperator
        public static string IN;
        //
        // Summary:
        //     Constant LE for DynamodbScanOperator
        public static string LE;
        //
        // Summary:
        //     Constant LT for DynamodbScanOperator
        public static string LT;
        //
        // Summary:
        //     Constant NE for DynamodbScanOperator
        public static string NE;
        //
        // Summary:
        //     Constant NOT_CONTAINS for DynamodbScanOperator
        public static string NOT_CONTAINS;
        //
        // Summary:
        //     Constant NOT_NULL for DynamodbScanOperator
        public static string NOT_NULL;
        //
        // Summary:
        //     Constant NULL for DynamodbScanOperator
        public static string NULL;
    }
}
