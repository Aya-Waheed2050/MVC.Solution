using DataAccess.Models.DepartmentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configurations
{
    internal class DepartmentConfigurations :BaseEntityConfigurations<Department> , IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasColumnType("varchar(20)");
            builder.Property(d => d.Code).HasColumnType("varchar(20)");

            builder.HasMany(d => d.Employees)
                   .WithOne(e => e.Department)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);

        }
    }
}
