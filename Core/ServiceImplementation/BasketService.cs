using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Baskets;
using ServiceAbstraction;
using Shared.BasketModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public  async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreateOrUpdate=await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreateOrUpdate is null)
                throw new Exception("Can not UPdate Or Create Basket Now ");
            else
                return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string Key)=> await _basketRepository.DeleteBasketAsync(Key);
       

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket=await _basketRepository.GetCustomerBasketAsync(Key);
            if (basket is not null)
                return  _mapper.Map<CustomerBasket,BasketDto>(basket);
            else
                throw new BasketNotFoundException(Key);
      
        }
    }
}
