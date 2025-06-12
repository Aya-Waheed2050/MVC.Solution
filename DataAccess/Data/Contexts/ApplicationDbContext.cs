using System.Reflection;
using DataAccess.Models.DepartmentModel;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.IdentityModel;
using DataAccess.Models.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<BaseEntity>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
         
    }
}
