using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.SHARED.Configs
{
    public class AwsConfig
    {
        public string ServiceUrl { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string FullImagePath { get; set; }
    }
}
