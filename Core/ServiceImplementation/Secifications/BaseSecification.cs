using DomainLayer.Common.Entities;
using DomainLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Secifications
{
    public abstract class BaseSecification<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public BaseSecification(Expression<Func<TEntity,bool>>? expression)
        {
            Criteria=expression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddIncludeExpressions(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        } 
        #endregion

        public Expression<Func<TEntity, object>> OrderByExp { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> expression) => OrderByExp = expression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression) => OrderByDescending = expression;
    }
}
