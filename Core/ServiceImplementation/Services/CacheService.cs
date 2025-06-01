using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceImplementation.Services
{
    internal class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)=> await _cacheRepository.GetAsync(key);
     

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var Value =JsonSerializer.Serialize(CacheValue);
            await _cacheRepository.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}
