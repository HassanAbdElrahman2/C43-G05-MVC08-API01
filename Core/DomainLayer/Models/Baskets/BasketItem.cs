using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Baskets
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string PictureUrl  { get; set; }=default!;
        public decimal price { get; set; }
        public int Quantity { get; set; }
    }
}
