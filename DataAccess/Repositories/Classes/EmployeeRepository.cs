using DataAccess.Data.Contexts;
using DataAccess.Models.EmployeeModel;

namespace DataAccess.Repositories.EmployeeRepo
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) 
        : GenericRepository<Employee>(_dbContext) ,IEmployeeRepository
    {
    }
}
