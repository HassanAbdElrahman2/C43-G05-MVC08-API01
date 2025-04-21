using DomainLayer.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations.Common
{
    internal class BaseConfiguration<TKey, TEntity> : IEntityTypeConfiguration<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<TKey>(E=>E.Id).UseIdentityColumn();
        }
    }
}
