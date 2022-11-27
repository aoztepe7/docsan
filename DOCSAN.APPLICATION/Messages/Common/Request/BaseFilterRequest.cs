using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Common.Request
{
    public class BaseFilterRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
