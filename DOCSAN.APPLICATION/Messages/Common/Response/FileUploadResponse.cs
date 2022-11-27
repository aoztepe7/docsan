using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Common.Response
{
    public class FileUploadResponse
    {
        public bool Success { get; set; }
        public string Url { get; set; }
    }
}
