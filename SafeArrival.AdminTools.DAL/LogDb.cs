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

        public List<Log> GetLogList(string env, string logType, string message)
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
            return helper.ScanTable("sa_logs",conditions);
        }
    }
}
