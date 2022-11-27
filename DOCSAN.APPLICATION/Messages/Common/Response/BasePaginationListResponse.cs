using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Common.Response
{
    public class BasePaginationListResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public BasePaginationListResponse(List<T> data, int count, int pageNumber, int pageSize, int code, string message)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Data = data;
            Code = code;
            Message = message;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
