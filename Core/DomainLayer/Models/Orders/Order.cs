using DomainLayer.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Orders
{
   public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, decimal subTotal, ICollection<OrderItem> items)
        {
            BuyerEmail = userEmail;
            shipToAddress = address;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            Items = items;
        }

        public string BuyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        
        
        public int DeliveryMethodId { get; set; } //FK
        public OrderStatus Status { get; set; }
       
       

        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Price; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

    }
}
