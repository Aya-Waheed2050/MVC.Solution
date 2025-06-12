using BusinessLogic.DataTransferObjects.EmployeeDtos;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, IWebHostEnvironment _environment, ILogger<EmployeesController> _logger)
           : Controller
    {
       
        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }

        [HttpGet]
        public IActionResult Search(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return PartialView("_EmployeeTablePartial", employees);
        }



        #region Create

        [HttpGet]
        public IActionResult Create() => View();
        

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel/*, IFormFile? imageFile*/)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Salary = employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image,
                    };
                    int result = _employeeService.CreateEmployee(employeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Employee not created");

                }
                catch (Exception e)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, e.Message);
                    else
                        _logger.LogError(e, "An error occurred while creating the employee.");
                }
            }
            return View(employeeViewModel);
        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ?  NotFound() : View(employee);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
            };

            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Salary = employeeViewModel.Salary,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    IsActive = employeeViewModel.IsActive,
                    HiringDate = employeeViewModel.HiringDate,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    DepartmentId = employeeViewModel.DepartmentId,
                };
                var result = _employeeService.UpdatedEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Employee is not Updated");
                return View(employeeViewModel);
            }
            catch (Exception e)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(employeeViewModel);
                }
                else
                {
                    _logger.LogError(e, "An error occurred while updating the employee.");
                    return View("error", e);
                }
            }

        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id.Value);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Deleted");
                    return RedirectToAction(nameof(Delete), new { id = id });
                }

            }
            catch (Exception e)
            {
                if (_environment.IsDevelopment())
                    return RedirectToAction(nameof(Index));
                else
                {
                    _logger.LogError(e.Message);
                    return View("Error", e);
                }
            }

        }

        #endregion
    }
}
