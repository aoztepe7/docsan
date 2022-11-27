using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Common.Response
{
    public class BaseIdResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public BaseIdResponse(Guid id, int code) : base(code)
        {
            Id = id;
        }
    }
}
