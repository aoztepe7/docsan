using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Dtos
{
    public class BasePaginationDto<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

        public BasePaginationDto(IEnumerable<T> data, int count)
        {
            TotalCount = count;
            Data = data;
        }
    }
}
