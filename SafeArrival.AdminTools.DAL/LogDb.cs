using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.DAL
{
    public class LogDb
    {
        private DynamoDBHelper<Log> helper;
        private string tableName = "sa_logs";
        public LogDb()
        {
            helper = new DynamoDBHelper<Log>();
        }
        public void Add(Log obj)
        {
            helper.CreateItem(tableName, obj);
        }

        public List<Log> GetLogList(string env, string logType, string message, DateTime from, DateTime to)
        {
            var conditions = new List<DynamodbScanCondition>();
            if (!string.IsNullOrEmpty(logType))
            {
                conditions.Add(new DynamodbScanCondition()
                {
                    AttributeName = "LogType",
                    Operator = DynamodbScanOperator.EQ,
                    Value = logType
                });
            }
            if (!string.IsNullOrEmpty(env))
            {
                conditions.Add(new DynamodbScanCondition()
                {
                    AttributeName = "LogKey",
                    Operator = DynamodbScanOperator.EQ,
                    Value = env
                });
            }
            if (!string.IsNullOrEmpty(message))
            {
                conditions.Add(new DynamodbScanCondition()
                {
                    AttributeName = "Message",
                    Operator = DynamodbScanOperator.CONTAINS,
                    Value = message
                });
            }
            if (from != null)
            {
                conditions.Add(new DynamodbScanCondition()
                {
                    AttributeName = "Date",
                    //Operator = "BETWEEN",
                    Operator = DynamodbScanOperator.GT,
                    //Value = new List<string> { from.ToString(), to.ToString()}
                    Value = new List<string> { from.ToString()}
                });
            }
            //if (to != null)
            //{
            //    conditions.Add(new DynamodbScanCondition()
            //    {
            //        AttributeName = "Date",
            //        Operator = "LT",
            //        Value = to
            //    });
            //}
            return helper.ScanTable("sa_logs", conditions);
        }
    }
}
