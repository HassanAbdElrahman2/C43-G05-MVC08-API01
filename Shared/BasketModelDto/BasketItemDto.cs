using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BasketModelDto
{
   public class BasketItemDto
    {
        public int Id { get; set; }
        public string  productName{ get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(1,double.MaxValue)]
        public decimal price { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
      

    }
}
