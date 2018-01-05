using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Model
{
    public class SA_S3Object
    {
        public string FullName { get; }

        public string S3BucketName { get; }

        public DateTime LastModified { get; }

        public long Size { get; }

        public SA_S3Object(string fullName, string s3BucketName, DateTime lastModified, long size)
        {
            FullName = fullName;
            S3BucketName = s3BucketName;
            LastModified = lastModified;
            Size = size;
        }
    }
}
