using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.RDS;
using Amazon.RDS.Model;
using SafeArrival.AdminTools.Model;
using Amazon.Lambda;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class RDSHelper : AwsHelperBase
    {

        private AmazonRDSClient client;

        public RDSHelper(string profile, string region, string color) : base(profile, region, color)
        {
            client = new AmazonRDSClient(
                CredentiaslManager.GetCredential(environment),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<SA_RdsInstance> GetRDSInstance()
        {
            var response = await client.DescribeDBInstancesAsync();
            var lstInstance = response.DBInstances;
            foreach (var instance in lstInstance)
            {
                if (instance.DBSubnetGroup.DBSubnetGroupName.IndexOf(environment.ToString()) >= 0)
                {
                    SA_RdsInstance objInstance = new SA_RdsInstance
                    {
                        DBInstanceIdentifier = instance.DBInstanceIdentifier,
                        DBInstanceArn = instance.DBInstanceArn,
                        RdsEnvinronment = environment,
                        Status = instance.DBInstanceStatus,
                        MultiAZ = instance.MultiAZ
                    };
                    return objInstance;
                }
            }
            return null;
        }

        public async Task<List<SA_RdsInstance>> GetRDSInstanceList()
        {
            var lst = new List<SA_RdsInstance>();
            var response = await client.DescribeDBInstancesAsync();
            var lstInstance = response.DBInstances;
            foreach (var instance in lstInstance)
            {

                SA_RdsInstance objInstance = new SA_RdsInstance
                {
                    DBInstanceIdentifier = instance.DBInstanceIdentifier,
                    DBInstanceArn = instance.DBInstanceArn,
                    RdsEnvinronment = environment,
                    Status = instance.DBInstanceStatus,
                    MultiAZ = instance.MultiAZ
                };
                lst.Add(objInstance);
            }
            return lst;
        }


        public async Task StopRdsInstance(string instanceIdentifier)
        {
            var instance = await GetRDSInstance();
            var isMultiAZ = instance.MultiAZ;
            if (isMultiAZ)
            {
                ModifyDBInstanceRequest request = new ModifyDBInstanceRequest();
                request.MultiAZ = false;
                request.ApplyImmediately = true;
                request.DBInstanceIdentifier = instanceIdentifier;
                try
                {
                    var response = await client.ModifyDBInstanceAsync(request);
                    isMultiAZ = response.DBInstance.MultiAZ;
                    while (isMultiAZ)
                    {
                        await Task.Delay(3000);
                        var newResponse = await GetRDSInstance();
                        isMultiAZ = newResponse.MultiAZ;
                    }
                }
                catch (Exception ex)
                {
                    ;
                }
            }


            var stopRequest = new StopDBInstanceRequest()
            {
                DBInstanceIdentifier = instanceIdentifier
            };
            await client.StopDBInstanceAsync(stopRequest);
        }

        public async Task StartRdsInstance(string instanceIdentifier, bool isMultyAz)
        {
            var startRequest = new StartDBInstanceRequest()
            {
                DBInstanceIdentifier = instanceIdentifier,
            };
            await client.StartDBInstanceAsync(startRequest);

            //ModifyDBInstanceRequest request = new ModifyDBInstanceRequest();
            //request.MultiAZ = isMultyAz;
            //request.DBInstanceIdentifier = instanceIdentifier;
            //var response = await client.ModifyDBInstanceAsync(request);
        }

        public async Task<string> TakeSnapshot()
        {
            var rdsInstance = await GetRDSInstance();
            var instanceId = rdsInstance.DBInstanceIdentifier;
            //don't wrap in using block or it will be disposed before you are done with it.
            var rdsClient = new AmazonRDSClient(
                CredentiaslManager.GetCredential(environment),
                AwsCommon.GetRetionEndpoint(region));
            var request = new CreateDBSnapshotRequest($"{environment}-{DateTime.Today.ToShortDateString()}", instanceId);
            //don't await this long running task
            var response = await rdsClient.CreateDBSnapshotAsync(request);
            return response.DBSnapshot.DBInstanceIdentifier;
        }
    }
}
