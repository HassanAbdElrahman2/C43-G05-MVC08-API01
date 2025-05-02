using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; } // Created From Customer [FrontEnd]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
