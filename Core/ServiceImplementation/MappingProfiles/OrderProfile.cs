using AutoMapper;
using DomainLayer.Models.Orders;
using Shared.IdentityDto;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(S => S.DeliveryMethod, O => O.MapFrom(D => D.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(S => S.ProductName, O => O.MapFrom(D => D.Product.ProductName))
                .ForMember(S=>S.PictureUrl,O=>O.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
