using DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configurations
{
    internal class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
