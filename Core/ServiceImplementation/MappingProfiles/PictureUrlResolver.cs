using AutoMapper;
using DomainLayer.Models.Products;
using Microsoft.Extensions.Configuration;
using Shared.DataTranssferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (source.PictureUrl is null)
                return String.Empty;
            else
            {
               var Image = $"{_configuration.GetSection("Urls")["BaseUrl"]}/{source.PictureUrl}";
                return Image;
            }
        }
    }
}
