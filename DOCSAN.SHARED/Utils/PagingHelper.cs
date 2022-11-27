using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.SHARED.Utils
{
    public static class PagingHelper
    {
        private static readonly int MAX_PAGE_SIZE = 10;
        public static IDictionary<string, int> GetPagingParameters(int pageSize, int pageNumber, bool discardMaxPageSize = false)
        {
            var resultMap = new Dictionary<string, int>();
            var finalPageSize = pageSize;
            if (!discardMaxPageSize)
                finalPageSize = pageSize > 0 && pageSize <= MAX_PAGE_SIZE ? pageSize : MAX_PAGE_SIZE;

            int skip = (pageNumber - 1) * finalPageSize;
            int take = finalPageSize;
            resultMap.Add("Skip", skip);
            resultMap.Add("Take", take);
            return resultMap;
        }
    }
}
