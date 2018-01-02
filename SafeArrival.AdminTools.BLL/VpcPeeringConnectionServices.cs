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
        EC2Helper ec2Helper = new EC2Helper(GlobalVariables.Enviroment, GlobalVariables.Region, GlobalVariables.Color);
        public async Task<AwsPeeringConnection> GetRdsPeeringConnection()
        {
            var response = await ec2Helper.GetPeeringConnection(Utils.FormatresourceName(AwsResourceTypeName.RdsPeeringConnection));
            return response;
        }

        public async Task<string> CreatePeeringConnection(AwsVpc requesterVpc, AwsVpc accepterVpc)
        {
            var peeringConnectionId = await ec2Helper.CreatePeeringConnection(
                AwsCommon.GetAwsAccountByEnvironment(GlobalVariables.Enviroment) ,
                requesterVpc.VpcId, accepterVpc.VpcId,
                Utils.FormatresourceName(AwsResourceTypeName.RdsPeeringConnection));
            await Task.Delay(5000);
            await ec2Helper.AcceptPeeringConnection(peeringConnectionId);
            await AddPeeringConnectionToRouteTables(requesterVpc, accepterVpc, peeringConnectionId);
            return peeringConnectionId;
        }

        public async Task DeletePeeringConnection(string vpcPeeringConnectionId, AwsVpc requesterVpc, AwsVpc accepterVpc)
        {
            await ec2Helper.DeletePeeringConnection(vpcPeeringConnectionId);
            await RemovePeeringConnectionFromRouteTables(requesterVpc, accepterVpc, vpcPeeringConnectionId);
        }

        private async Task AddPeeringConnectionToRouteTables(AwsVpc requesterVpc, AwsVpc accepterVpc, string peerConnectionId)
        {
            //Set requester
            var requesterRouteTables = await ec2Helper.GetRouteTablesByVpc(requesterVpc.VpcId);
            foreach (var routeTable in requesterRouteTables)
            {
                ec2Helper.CreateRouteForRouteTable(peerConnectionId, null, null, accepterVpc.CidrBlock, routeTable);
            }

            var accepterRouteTables = await ec2Helper.GetRouteTablesByVpc(accepterVpc.VpcId);
            foreach (var routeTable in accepterRouteTables)
            {
                ec2Helper.CreateRouteForRouteTable(peerConnectionId, null, null, requesterVpc.CidrBlock, routeTable);
            }
        }

        private async Task RemovePeeringConnectionFromRouteTables(AwsVpc requesterVpc, AwsVpc accepterVpc, string peerConnectionId)
        {
            //Set requester
            var requesterRouteTables = await ec2Helper.GetRouteTablesByVpc(requesterVpc.VpcId);
            foreach (var routeTable in requesterRouteTables)
            {
                await ec2Helper.DeleteRouteForRouteTable(accepterVpc.CidrBlock, routeTable);
            }

            var accepterRouteTables = await ec2Helper.GetRouteTablesByVpc(requesterVpc.VpcId);
            foreach (var routeTable in accepterRouteTables)
            {
                await ec2Helper.DeleteRouteForRouteTable(requesterVpc.CidrBlock, routeTable);
            }
        }

        public async Task<List<AwsVpc>> GetAvailablePeeringVpcList()
        {
            var response = await ec2Helper.GetVPCList();
            return response;
        }
    }
}
