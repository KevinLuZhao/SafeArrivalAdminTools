using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class ClientFactory <T>
    {
        private static T client;
        public static T CreateClient()
        {
            var credentials = new AssumeRoleAWSCredentials(new StoredProfileAWSCredentials(), "arn:aws:iam::383514732995:role/SAFE-Dev_role", "AssumeRole");
            //client = new T(credentials, AwsCommon.GetRetionEndpoint("us-east-2"));
            return client;
        }
    }
}
