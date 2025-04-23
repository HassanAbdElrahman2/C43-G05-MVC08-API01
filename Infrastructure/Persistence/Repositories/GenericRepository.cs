using DomainLayer.Common.Entities;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDBContext _dBContext;

        public GenericRepository( StoreDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dBContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dBContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await _dBContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)=>_dBContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _dBContext.Set<TEntity>().Remove(entity);
   
    }
}
