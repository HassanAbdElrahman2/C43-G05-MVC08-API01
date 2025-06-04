using AutoMapper;
using DomainLayer.Models.Products;
using Shared.DataTranssferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest=>dest.productType,option=>option.MapFrom(Scr=>Scr.ProductType.Name))
                .ForMember(dest => dest.productBrand, option => option.MapFrom(Src => Src.ProductBrand.Name))
                .ForMember(dest=>dest.PictureUrl,option=>option.MapFrom<PictureUrlResolver>());
            CreateMap<ProductType, TypeDTO>();
            CreateMap<ProductBrand, BrandDTO>();

        }
    }
}
