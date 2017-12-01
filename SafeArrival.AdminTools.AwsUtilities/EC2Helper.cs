using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class EC2Helper
    {
        private SafeArrival.AdminTools.Model.Environment Environment { get; }
        private AmazonEC2Client client;

        public EC2Helper(Model.Environment profile, string region)
        {
            //Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials(profile.ToString());
            this.Environment = profile;
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonEC2Client(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }
        /****************************************** VPC  ******************************************/
        public async Task<List<AwsVpc>> GetVPCList()
        {
            var request = new DescribeVpcsRequest();
            var response = await client.DescribeVpcsAsync(request);
            var ret = new List<AwsVpc>();
            foreach (var vpc in response.Vpcs)
            {
                ret.Add(new AwsVpc()
                {
                    CidrBlock = vpc.CidrBlock,
                    Name = vpc.Tags.Find(o => o.Key == "Name").Value,
                    State = vpc.State,
                    VpcId = vpc.VpcId
                });
            }
            return ret;
        }
        /****************************************** VPC Peering Connection ******************************************/
        public async Task<AwsPeeringConnection> GetPeeringConnection(string name)
        {
            var request = new DescribeVpcPeeringConnectionsRequest();
            var response = await client.DescribeVpcPeeringConnectionsAsync(request);
            var connection = response.VpcPeeringConnections.
                FindAll(o => (o.Tags.FindIndex(p => p.Key == "Name" && p.Value == name) >= 0)).
                Find(o=>o.Status.Code == VpcPeeringConnectionStateReasonCode.Active);
            if (connection != null)
            {
                var connectionModel = new AwsPeeringConnection()
                {
                    AccepterVpc = connection.AccepterVpcInfo.VpcId,
                    RequesterVpc = connection.RequesterVpcInfo.VpcId,
                    VpcPeeringConnectionId = connection.VpcPeeringConnectionId,
                    Status = connection.Status.Code.Value
                };
                return connectionModel;
            }
            else
                return null;
        }

        public async Task<string> DeletePeeringConnection(string peeringConnectionId)
        {
            try
            {
                var request = new DeleteVpcPeeringConnectionRequest()
                {
                    VpcPeeringConnectionId = peeringConnectionId
                };
                var response = await client.DeleteVpcPeeringConnectionAsync(request);
                return $"VPC Peering Connection {peeringConnectionId} is deleted successfully";
            }
            catch (Exception ex)
            {
                return $"Error -- {ex.Message}";
            }
        }

        public async Task<string> CreatePeeringConnection(string owerId, string accepterVpc, string requesterVpc, string name)
        {
            var request = new CreateVpcPeeringConnectionRequest()
            {
                PeerOwnerId = owerId,
                PeerVpcId = accepterVpc,
                VpcId = requesterVpc
            };
            var response = await client.CreateVpcPeeringConnectionAsync(request);
            AssignNameToResource(response.VpcPeeringConnection.VpcPeeringConnectionId, name);
            return response.VpcPeeringConnection.VpcPeeringConnectionId;
        }

        public async Task<string> AcceptPeeringConnection(string vpcPeeringConnectionId)
        {
            var request = new AcceptVpcPeeringConnectionRequest()
            {
                VpcPeeringConnectionId = vpcPeeringConnectionId
            };
            var response = await client.AcceptVpcPeeringConnectionAsync(request);
            return response.VpcPeeringConnection.VpcPeeringConnectionId;
        }

        internal RouteTable FindRouteTable(string routeTableName)
        {
            RouteTable ret = null;
            var response = client.DescribeRouteTables();
            foreach (var item in response.RouteTables)
            {
                if (item.Tags.FindIndex(o => o.Key == "Name" && o.Value == routeTableName) >= 0)
                {
                    ret = item;
                    break;
                }
            }
            return ret;
        }

        /****************************************** Route Table  ******************************************/
        public async Task<List<string>> GetRouteTablesByVpc(string vpcId)
        {
            var ret = new List<string>();
            var request = new DescribeRouteTablesRequest();
            var response = await client.DescribeRouteTablesAsync(request);
            foreach (var routeTable in response.RouteTables)
            {
                if (routeTable.VpcId == vpcId && routeTable.Associations.Count > 0)
                    ret.Add(routeTable.RouteTableId);
            }
            return ret;
        }

        public bool CreateRouteForRouteTable(
            string peeringConnection,
            string gatewayId,
            string natGatewayId,
            string destinationCidrBlock,
            string routeTableId)
        {
            var request = new CreateRouteRequest();
            if (!string.IsNullOrEmpty(gatewayId))
                request.GatewayId = gatewayId;
            if (!string.IsNullOrEmpty(natGatewayId))
                request.NatGatewayId = natGatewayId;
            if (!string.IsNullOrEmpty(peeringConnection))
                request.VpcPeeringConnectionId = peeringConnection;
            request.DestinationCidrBlock = destinationCidrBlock;
            request.RouteTableId = routeTableId;

            return client.CreateRoute(request).Return;
        }

        public async Task DeleteRouteForRouteTable(string destinationCidrBlock, string routeTableId)
        {
            try
            {
                var request = new DeleteRouteRequest()
                {
                    DestinationCidrBlock = destinationCidrBlock,
                    RouteTableId = routeTableId
                };

                await client.DeleteRouteAsync(request);
            }
            catch (Exception ex)
            {
                //Check if destinationCidrBlock doesn't exist, should skip
                throw ex;
            }
        }

        /****************************************** Other  ******************************************/
        private void AssignNameToResource(string resourceId, string name)
        {
            //string resourceName = AwsCommon.FormatresourceName(resourceTypeName);
            CreateTagsRequest reqCreateTag = new CreateTagsRequest();
            reqCreateTag.Resources = new List<string>();
            reqCreateTag.Resources.Add(resourceId);
            reqCreateTag.Tags = new List<Tag>();
            reqCreateTag.Tags.Add(new Tag("Name", name));

            client.CreateTags(reqCreateTag);
        }
    }
}
