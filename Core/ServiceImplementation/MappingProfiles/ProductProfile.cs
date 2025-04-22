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
                .ForMember(dest=>dest.TypeName,option=>option.MapFrom(Scr=>Scr.ProductType.Name))
                .ForMember(dest => dest.BrandName, option => option.MapFrom(Src => Src.ProductBrand.Name));
            CreateMap<ProductType, TypeDTO>();
            CreateMap<ProductBrand, BrandDTO>();

        }
    }
}
