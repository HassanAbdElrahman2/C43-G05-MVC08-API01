using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.OpderException;
using DomainLayer.Exceptions.ProductException;
using DomainLayer.Models.Orders;
using DomainLayer.Models.Products;
using ServiceAbstraction;
using Shared.IdentityDto;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Services
{
    public class OrderService(IMapper _mapper,IUnitOfWork _unitOfWork,IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
        {
            var AddressOrder = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            var DeliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>()
                .GetByIdAsync(orderDto.DevliveryMethodId) ?? throw new DeliveryMethodNotFoundException(orderDto.DevliveryMethodId);

            var Basket=await _basketRepository.GetCustomerBasketAsync(orderDto.BasketId)
                ??throw new BasketNotFoundException(orderDto.BasketId);

            List<OrderItem> OrderItems = [];
            var ProductRepository = _unitOfWork.GetRepository<int, Product>();

            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepository.GetByIdAsync(item.Id)??throw new ProductNotFound(item.Id) ;

                OrderItems.Add(new OrderItem()
                {
                    Price = Product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrder() 
                    { PictureUrl = Product.PictureUrl, ProductId = Product.Id, ProductName = Product.Name }
                });
            }
            var SubTotal = OrderItems.Sum(I => I.Price * I.Quantity);

            var Order = new Order(Email, AddressOrder, DeliveryMethod, SubTotal, OrderItems);

            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(Order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }
    }
}
