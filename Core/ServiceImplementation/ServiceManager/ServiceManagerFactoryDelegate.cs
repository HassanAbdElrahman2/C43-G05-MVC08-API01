using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.ServiceManager
{
    public class ServiceManagerFactoryDelegate
        (Func<IProductService> ProductProvidor, Func<IBasketService> BasketProvidor
        ,Func<IAuthenticationService> AuthenticationProvidor,Func<IOrderService> OrderProvidor) : IServiceManager
    {
        public IProductService ProductService => ProductProvidor.Invoke();
        public IBasketService BasketService => BasketProvidor.Invoke();
        public IAuthenticationService AuthenticationService => AuthenticationProvidor.Invoke();
        public IOrderService OrderService => OrderProvidor.Invoke();
    }
}
