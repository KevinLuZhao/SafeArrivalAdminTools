﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.RDS;
using Amazon.RDS.Model;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class RDSHelper
    {
        public SafeArrival.AdminTools.Model.Environment Environment { get; }
        private AmazonRDSClient client;

        public RDSHelper(Model.Environment profile, string region)
        {
            //Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials(profile.ToString());
            this.Environment = profile;
            //client = new AmazonRDSClient(credentials, AwsCommon.GetRetionEndpoint(region));
            client = new AmazonRDSClient(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<AwsRdsInstance> GetRDSInstance()
        {
            var response = await client.DescribeDBInstancesAsync();
            var lstInstance = response.DBInstances;
            foreach (var instance in lstInstance)
            {
                if (instance.DBSubnetGroup.DBSubnetGroupName.IndexOf(this.Environment.ToString()) >= 0)
                {
                    AwsRdsInstance objInstance = new AwsRdsInstance
                    {
                        DBInstanceIdentifier = instance.DBInstanceIdentifier,
                        DBInstanceArn = instance.DBInstanceArn,
                        RdsEnvinronment = this.Environment,
                        Status = instance.DBInstanceStatus,
                        MultiAZ = instance.MultiAZ
                    };
                    return objInstance;
                }
            }
            return null;
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
                        System.Threading.Thread.Sleep(30000);
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

        public async Task StartRdsInstance(string instanceIdentifier)
        {
            var startRequest = new StartDBInstanceRequest()
            {
                DBInstanceIdentifier = instanceIdentifier
            };
            await client.StartDBInstanceAsync(startRequest);

            ModifyDBInstanceRequest request = new ModifyDBInstanceRequest();
            request.MultiAZ = true;
            request.DBInstanceIdentifier = instanceIdentifier;
            var response = await client.ModifyDBInstanceAsync(request);
        }
    }
}
