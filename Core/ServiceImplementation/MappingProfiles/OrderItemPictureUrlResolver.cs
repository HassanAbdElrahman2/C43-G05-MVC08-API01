using AutoMapper;
using DomainLayer.Models.Orders;
using Microsoft.Extensions.Configuration;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (source.Product.PictureUrl is null)
                return String.Empty;
            else
            {
                var Image = $"{_configuration.GetSection("Urls")["BaseUrl"]}/{source.Product.PictureUrl}";
                return Image;
            }
        }
    }
}
