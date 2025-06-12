using AutoMapper;
using BusinessLogic.DataTransferObjects.EmployeeDtos;
using BusinessLogic.Services.AttachmentService;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModel;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper, IAttachmentService _attachmentService) 
        : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employee = _unitOfWork.EmployeeRepository.GetAll();
            else
                employee = _unitOfWork.EmployeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employee);
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }
    }
}
