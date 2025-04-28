using DomainLayer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity,TKey>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get; }
        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get;  }

        public Expression<Func<TEntity,object>> OrderByExp { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }

        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; set; }
    }
}
