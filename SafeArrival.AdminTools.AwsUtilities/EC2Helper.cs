using Amazon.EC2;
using Amazon.EC2.Model;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class EC2Helper : AwsHelperBase
    {
        private AmazonEC2Client client;

        public EC2Helper(Model.Environment profile, string region, string color) : base(profile, region, color)
        {
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonEC2Client(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }
        /****************************************** VPC  ******************************************/
        public async Task<List<SA_Vpc>> GetVPCList()
        {
            var request = new DescribeVpcsRequest();
            var response = await client.DescribeVpcsAsync(request);
            var ret = new List<SA_Vpc>();
            foreach (var vpc in response.Vpcs)
            {
                if (vpc.IsDefault)
                {
                    continue;
                }
                ret.Add(new SA_Vpc()
                {
                    CidrBlock = vpc.CidrBlock,
                    Name = vpc.Tags.Find(o => o.Key == "Name").Value,
                    State = vpc.State,
                    VpcId = vpc.VpcId
                });
            }
            return ret;
        }

        public async Task<SA_Vpc> GetVPC()
        {
            //var filters = new List<Filter>();
            //var filter = new Filter("Name", new List<string> { environment.ToString() });
            //filters.Add(filter);
            //var request = new DescribeVpcsRequest()
            //{
            //    Filters = filters
            //};
            //Vpc
            var response = await client.DescribeVpcsAsync();
            var vpc = response.Vpcs.Find(o=>o.Tags.Find(p=>p.Key=="Name").Value==environment.ToString());
            if (vpc == null)
                return null;
            var awsVpc = new SA_Vpc()
            {
                CidrBlock = vpc.CidrBlock,
                Name = vpc.Tags.Find(o => o.Key == "Name").Value,
                State = vpc.State,
                VpcId = vpc.VpcId
            };
            return awsVpc;
        }

        public async Task<List<SA_Subnet>> GetSubnetList()
        {
            var saSubnets = new List<SA_Subnet>();
            var request = new DescribeSubnetsRequest();
            var response = await client.DescribeSubnetsAsync();
            foreach (var awsSubnet in response.Subnets)
            {
                if (awsSubnet.Tags.Find(o => o.Key == "Name").Value.IndexOf(environment.ToString()) == 0)
                {
                    saSubnets.Add(ConvertSubnet(awsSubnet));
                }
            }
            return saSubnets;
        }

        private SA_Subnet ConvertSubnet(Subnet awsSubnet)
        {
            SA_Subnet saSubnet = ModelTransformer<Subnet, SA_Subnet>.TransformAwsModelToSafeArrivalModel(awsSubnet);
            saSubnet.DisplayName = awsSubnet.Tags.Find(o => o.Key == "Name").Value;
            return saSubnet;
        }

        public async Task<List<SA_SecurityGroup>> GetSecurityGroupList()
        {

            var saSecurityGroups = new List<SA_SecurityGroup>();
            var request = new DescribeSecurityGroupsRequest();
            var response = await client.DescribeSecurityGroupsAsync(request);
            foreach (var securityGroup in response.SecurityGroups)
            {
                //Only Security group name follow naming convention and in current environment will be selected
                if (securityGroup.Tags.Find(o => o.Key == "Name") == null)
                    continue;
                if (securityGroup.Tags.Find(o => o.Key == "Name").Value.IndexOf(environment.ToString()) == 0)
                {
                    saSecurityGroups.Add(ConvertSecurityGroup(securityGroup));
                }
            }
            return saSecurityGroups;
        }

        private SA_SecurityGroup ConvertSecurityGroup(SecurityGroup awsSecurityGroup)
        {
            try
            {
                SA_SecurityGroup saSecurityGroup = ModelTransformer<SecurityGroup, SA_SecurityGroup>.TransformAwsModelToSafeArrivalModel(awsSecurityGroup);
                saSecurityGroup.DisplayName = awsSecurityGroup.Tags.Find(o => o.Key == "Name").Value;
                return saSecurityGroup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /****************************************** VPC Peering Connection ******************************************/
        public async Task<SA_PeeringConnection> GetPeeringConnection(string name)
        {
            var request = new DescribeVpcPeeringConnectionsRequest();
            var response = await client.DescribeVpcPeeringConnectionsAsync(request);
            var connection = response.VpcPeeringConnections.
                FindAll(o => (o.Tags.FindIndex(p => p.Key == "Name" && p.Value == name) >= 0)).
                Find(o => o.Status.Code == VpcPeeringConnectionStateReasonCode.Active);
            if (connection != null)
            {
                var connectionModel = new SA_PeeringConnection()
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
                if (routeTable.VpcId == vpcId && routeTable.Tags.Find(o => o.Key == "Name") != null)
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
