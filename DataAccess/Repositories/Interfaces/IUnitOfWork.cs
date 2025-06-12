using DataAccess.Repositories.EmployeeRepo;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        int SaveChanges();
    }
}
