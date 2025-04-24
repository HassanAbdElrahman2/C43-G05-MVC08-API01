using DomainLayer.Common.Entities;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> entities,ISpecifications<TEntity,TKey> specifications)
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var Query = entities;
            if(specifications.Criteria is not null)
                Query = Query.Where(specifications.Criteria);
            if (specifications.OrderByDescending is not null)
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            if (specifications.OrderByExp is not null)
                Query = Query.OrderBy(specifications.OrderByExp);
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, Ex) => CurrentQuery.Include(Ex));
            }

            return Query;
        }
    }
}
