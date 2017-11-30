using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.BLL
{
    public class VpcPeeringConnectionServices
    {
        public async Task<AwsPeeringConnection> GetRdsPeeringConnection()
        {
            var helper = new EC2Helper(GlobalVariables.Enviroment, GlobalVariables.Region);
            var response = await helper.GetPeeringConnection(Utils.FormatresourceName(AwsResourceTypeName.RdsPeeringConnection));
            return response;
        }

        public async Task CreatePeeringConnection()
        {
            var helper = new EC2Helper(GlobalVariables.Enviroment, GlobalVariables.Region);
            var responseRequest = await helper.CreatePeeringConnection(
                AwsCommon.GetAwsAccountByEnvironment(GlobalVariables.Enviroment) ,
                "vpc-bfba51d7", "vpc-3411fe5c",
                Utils.FormatresourceName(AwsResourceTypeName.RdsPeeringConnection));
            System.Threading.Thread.Sleep(5000);
            var responseAccept = await helper.AcceptPeeringConnection(responseRequest);
        }
    }
}
