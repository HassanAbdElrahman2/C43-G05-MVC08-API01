﻿using DomainLayer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Products
{
    public class Product : BaseEntity<int>
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public int TypeId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;
        public ProductType ProductType { get; set; } = null!;


    }
}
