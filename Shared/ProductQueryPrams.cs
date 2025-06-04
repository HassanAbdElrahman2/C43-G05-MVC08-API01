using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryPrams
    {
        private const int minPageSize=5;
        private const int maxPageSize=10;
       public int? BrandId { get;set; }
       public int? TypeId { get; set; }
       public ProductSortingOptions sort { get; set; }
       public string? search { get; set; }
       private int pageSize=minPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value>maxPageSize?maxPageSize:value; }
        }

        public int pageNumber { get; set; } = 1;

    }
}
