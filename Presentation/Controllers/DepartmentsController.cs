using BusinessLogic.DataTransferObjects.DepartmentDtos;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class DepartmentsController (IDepartmentService _departmentService ,ILogger<DepartmentsController> _logger ,IWebHostEnvironment _environment)
        : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }


        #region Create

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation
                    };
                    int result = _departmentService.AddDepartment(departmentDto);
                    string Message;
                    if (result > 0)
                        Message = $"Department {departmentViewModel.Name} Is Created Successfully";
                    else
                        Message = $"Department {departmentViewModel.Name} Can't be Created ";

                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index));                 
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(departmentViewModel);
        }

        #endregion

        #region Details Of Department

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }


        #endregion

        #region Edit Department


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };

            return View(departmentViewModel);
        }


        [HttpPost]
        public IActionResult Edit([FromRoute]int id , DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedDepartmentDto = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation
                    };
                    int result = _departmentService.UpdateDepartment(updatedDepartmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));

                    else
                        ModelState.AddModelError(string.Empty, "Something went wrong while updating the department. Please try again later.");

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError("ErrorView", ex.Message);
                }
            }
                
            return View(viewModel);
            
        }


        #endregion

        #region Delete Department

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong while deleting the department. Please try again later.");
                    return RedirectToAction(nameof(Delete), new { id });
                }

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView" , ex);
                }
            }    
        }


        #endregion

    }
}
