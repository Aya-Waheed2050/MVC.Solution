using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DataTransferObjects.DepartmentDtos
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name Is Required !!!")]
        public string Name { get; set; } = null!;
        
        [Required]
        [Range(100 , int.MaxValue)]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }


    }
}
