using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryPrams
    {
       public int? BrandId { get;set; }
       public int? TypeId { get; set; }
       public ProductSortingOptions sortingOption { get; set; }
    }
}
