using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class IAMHelper
    {
        public void GetCurrentUser()
        {
            var client = new AmazonIdentityManagementServiceClient();
            //var response = client.GetUser();
            //var c = response.User;
        }
    }
}
