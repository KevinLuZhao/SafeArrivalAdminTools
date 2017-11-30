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

        public async Task<AwsPeeringConnection> GetPeeringConnection(string name)
        {
            var request = new DescribeVpcPeeringConnectionsRequest();
            var response = await client.DescribeVpcPeeringConnectionsAsync(request);
            var connection = response.VpcPeeringConnections.Find(o => o.Tags.FindIndex(p => p.Key == "Name" && p.Value == name) >= 0);
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
                PeerOwnerId = "125237747044",
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

        internal bool CreateRouteForRouteTable(
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

        internal async Task DeleteRouteForRouteTable(string destinationCidrBlock, string routeTableId)
        {
            var request = new DeleteRouteRequest()
            {
                DestinationCidrBlock = destinationCidrBlock,
                RouteTableId = routeTableId
            };

            await client.DeleteRouteAsync(request);
        }

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
