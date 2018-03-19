using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Route53;
using Amazon.Route53.Model;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class Route53Helper : AwsHelperBase
    {
        private AmazonRoute53Client client;

        public Route53Helper(string profile, string region, string color) : base(profile, region, color)
        {
            client = new AmazonRoute53Client(
                CredentiaslManager.GetCredential(profile),
                AwsCommon.GetRetionEndpoint(region));
        }

        public async Task<string> GetHostZoneId()
        {
            var account = AwsCommon.GetEnvironmentAccounts()[environment];
            var request = new ListHostedZonesByNameRequest()
            {
                DNSName = account.DNS + "."
            };
            var response = await client.ListHostedZonesByNameAsync(request);
            return response.HostedZones.Find(o => o.Name == account.DNS + ".").Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostzoneId">Sample: /hostedzone/ZW9CIJG5IAQYD</param>
        /// <param name="resourceRecordSetName">Sample api.infra.safearrival.testschoolmessenger.com.</param>
        /// <returns>Example: infra-Admin-ALB-747523918.us-east-2.elb.amazonaws.com (Regular)Or
        /// admin.infra.safearrival.testschoolmessenger.com.s3-website.us-east-2.amazonaws.com (Maintenance)</returns>
        public async Task<List<string>> GetRecorSetValues(string hostzoneId, List<string> resourceRecordSetNames)
        {
            var ret = new List<string>();
            var request = new ListResourceRecordSetsRequest()
            {
                HostedZoneId = hostzoneId,
                //StartRecordName = resourceRecordSetName
            };
            var response = await client.ListResourceRecordSetsAsync(request);
            foreach (var name in resourceRecordSetNames)
            {
                var recordSet = response.ResourceRecordSets.Find(o => o.Name == name);
                if (recordSet != null && recordSet.ResourceRecords.Count > 0)
                {
                    ret.Add(recordSet.ResourceRecords[0].Value);
                }
            }
            return ret;
        }

        public async Task<string> UpdateHostZoneRecordSetValue(string action, List<KeyValuePair<string, string>> recordsetChanges)
        {
            List<Change> changes = new List<Change>();
            foreach (var recordsetChange in recordsetChanges)
            {
                changes.Add(
                    new Change(
                        new ChangeAction(action),  //"UPSERT"
                        new ResourceRecordSet()
                        {
                            Name = recordsetChange.Key,
                            TTL = 900,
                            Type = RRType.CNAME,
                            ResourceRecords = new List<ResourceRecord>()
                            {
                                new ResourceRecord()
                                {
                                    Value = recordsetChange.Value
                                }
                            }
                        }
                    )
                );
            };
            ChangeBatch batch = new ChangeBatch(changes);
            var request = new ChangeResourceRecordSetsRequest()
            {
                HostedZoneId = await GetHostZoneId(),
                ChangeBatch = batch
            };
            var response = await client.ChangeResourceRecordSetsAsync(request);
            return response.HttpStatusCode.ToString();
        }
    }
}
