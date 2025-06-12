using BusinessLogic.DataTransferObjects.DepartmentDtos;
using DataAccess.Models.DepartmentModel;

namespace BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department d)
        {
            return new DepartmentDto()
            {
                DeptId = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = DateOnly.FromDateTime((DateTime)d.CreatedOn)
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department d)
        {
            return new DepartmentDetailsDto()
            {
                Id = d.Id,
                Name = d.Name,
                CreatedOn = DateOnly.FromDateTime((DateTime)d.CreatedOn),
                Code = d.Code,
                Description = d.Description,
                CreatedBy = d.CreatedBy,
                LastModifiedBy = d.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime((DateTime)d.LastModifiedOn),
                IsDeleted = d.IsDeleted
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
        {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
        };
        


    }
}
