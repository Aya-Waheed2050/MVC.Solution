using BusinessLogic.DataTransferObjects.EmployeeDtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employee);
        int UpdatedEmployee(UpdatedEmployeeDto employee);
        bool DeleteEmployee(int id);
    }
}
