using DomainLayer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TKey : IEquatable<TKey>
            where TEntity : BaseEntity<TKey>;
        public Task CompleteAsync();
    }
}
