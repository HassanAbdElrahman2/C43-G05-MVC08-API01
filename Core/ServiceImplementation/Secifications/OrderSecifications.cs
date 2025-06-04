using DomainLayer.Models.Orders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Secifications
{
    public class OrderSecifications:BaseSecification<Order,Guid>
    {
        public OrderSecifications(string Email):base(E=>E.BuyerEmail==Email)
        {
            AddIncludeExpressions(E => E.DeliveryMethod);
            AddIncludeExpressions(E => E.Items);
            AddOrderByDescending(O=>O.OrderDate);
        }
        public OrderSecifications(Guid Id) : base(E => E.Id == Id)
        {
            AddIncludeExpressions(E => E.DeliveryMethod);
            AddIncludeExpressions(E => E.Items);
        }
    }
}
