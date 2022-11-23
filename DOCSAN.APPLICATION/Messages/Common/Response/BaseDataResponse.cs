using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Common.Response
{
    public class BaseDataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public BaseDataResponse(T? data, int code) : base(code)
        {
            Data = data;
        }
    }
}
