﻿namespace BusinessLogic.DataTransferObjects.DepartmentDtos
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int CreatedBy { get; set; }
        public DateOnly CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }



    }
}
