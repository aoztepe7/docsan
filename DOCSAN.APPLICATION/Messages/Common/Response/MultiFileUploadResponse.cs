using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Common.Response
{
    public class MultiFileUploadResponse
    {
        public bool Success { get; set; }
        public List<string> Urls { get; set; } = new List<string>();
    }
}
