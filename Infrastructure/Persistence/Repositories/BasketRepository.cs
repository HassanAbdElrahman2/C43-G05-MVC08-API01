using DomainLayer.Contracts;
using DomainLayer.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database=connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? time = null)
        {
           var BasketString=JsonSerializer.Serialize(basket);
            bool IsCreateOrUpdate=await _database.StringSetAsync(basket.Id,BasketString,time??TimeSpan.FromDays(30));
            if (IsCreateOrUpdate)
                return await GetCustomerBasketAsync(basket.Id);
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string id) =>await _database.KeyDeleteAsync(id);
       

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string Key)
        {
           var Basket=await _database.StringGetAsync(Key);
            if(Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
