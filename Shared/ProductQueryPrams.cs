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
       public ProductSortingOptions sortingOption { get; set; }
       public string? SearchValue { get; set; }
        private int _PageSize=minPageSize;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value>maxPageSize?maxPageSize:value; }
        }

        public int PageIndex { get; set; } = 1;

    }
}
