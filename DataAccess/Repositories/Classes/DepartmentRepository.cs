using DataAccess.Data.Contexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext _dbContext)
        : GenericRepository<Department>(_dbContext), IDepartmentRepository
    {
    }

}

