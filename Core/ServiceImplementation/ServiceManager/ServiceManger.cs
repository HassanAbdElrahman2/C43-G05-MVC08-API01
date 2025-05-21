using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using ServiceImplementation.IdentityService;
using ServiceImplementation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.ServiceManager
{
    public class ServiceManger(IUnitOfWork _unitOfWork,IMapper _mapper ,IBasketRepository _basketRepository,UserManager<ApplicationUser> userManager, IConfiguration configuration) : IServiceManager
    {
        
        private readonly Lazy<IProductService>  _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _productService.Value;
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketService=>_LazyBasketService.Value; 
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,configuration,_mapper));
        public IAuthenticationService AuthenticationService { get => _LazyAuthenticationService.Value; }

        private readonly Lazy<IOrderService> _orderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _unitOfWork, _basketRepository));
        public IOrderService OrderService { get => _orderService.Value; }
    }
}
