﻿using DomainLayer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = default!;
        public decimal Price  { get; set; }
        public int Quantity{ get; set; }

    }
}
