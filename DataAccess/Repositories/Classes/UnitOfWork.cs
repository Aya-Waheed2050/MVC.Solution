using DataAccess.Data.Contexts;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.EmployeeRepo;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges() => _dbContext.SaveChanges();

    }
}
