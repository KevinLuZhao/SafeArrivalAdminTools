using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_Snapshot
    {
        //
        // Summary:
        //     Gets and sets the property SnapshotId.
        //     The ID of the snapshot. Each snapshot receives a unique identifier when it is
        //     created.
        public string SnapshotId { get; set; }
        //
        // Summary:
        //     Gets and sets the property OwnerId.
        //     The AWS account ID of the EBS snapshot owner.
        public string OwnerId { get; set; }
        //
        // Summary:
        //     Gets and sets the property StartTime.
        //     The time stamp when the snapshot was initiated.
        public DateTime StartTime { get; set; }
        //
        // Summary:
        //     Gets and sets the property StateMessage.
        //     Encrypted Amazon EBS snapshots are copied asynchronously. If a snapshot copy
        //     operation fails (for example, if the proper AWS Key Management Service (AWS KMS)
        //     permissions are not obtained) this field displays error state details to help
        //     you diagnose why the error occurred. This parameter is only returned by the DescribeSnapshots
        //     API operation.
        public string StateMessage { get; set; }
        //
        // Summary:
        //     Gets and sets the property VolumeSize.
        //     The size of the volume, in GiB.
        public int VolumeSize { get; set; }

        public string Name { get; set; }
    }
}
