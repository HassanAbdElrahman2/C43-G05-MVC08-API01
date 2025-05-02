using Shared.BasketModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);
        public Task<bool> DeleteBasketAsync(string Key);
        public Task<BasketDto>GetBasketAsync(string Key);
    }
}
