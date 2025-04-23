using DomainLayer.Common.Entities;
using DomainLayer.Contracts;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly StoreDBContext _dBContext;
        private readonly Dictionary<string,object> _repositories=[];
        public UnitOfWork(StoreDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task CompleteAsync()
        {
           await _dBContext.SaveChangesAsync();
        }

        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>()
            where TKey : IEquatable<TKey>
            where TEntity : BaseEntity<TKey>
        {
            // Get name Of Taype
            var Type=typeof(TEntity).Name;
            if(_repositories.ContainsKey(Type))
                return (IGenericRepository<TKey,TEntity>) _repositories[Type];
            else
            {
                var Rep= new GenericRepository<TKey,TEntity>(_dBContext);
                _repositories.Add(Type, Rep);
                return Rep;
            }
            
        }
    }
}
