using AutoMapper;
using BusinessLogic.DataTransferObjects.EmployeeDtos;
using DataAccess.Models.EmployeeModel;

namespace BusinessLogic.Profiles
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee , EmployeeDto>()
                .ForMember(dest => dest.EmployeeType , options => options.MapFrom(scr => scr.EmployeeType))
                .ForMember(dest => dest.Gender , options => options.MapFrom(scr => scr.Gender))
                .ForMember(dest => dest.Department , options => options.MapFrom(src => src.Department != null? src.Department.Name : null));


            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(scr => scr.EmployeeType))
                .ForMember(dest => dest.Gender, options => options.MapFrom(scr => scr.Gender))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.Image, options => options.MapFrom(src => src.ImageName));


            CreateMap<CreatedEmployeeDto , Employee>()
                 .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto , Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
        }

    }
}
