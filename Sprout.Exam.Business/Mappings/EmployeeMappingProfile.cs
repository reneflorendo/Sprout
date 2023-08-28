using AutoMapper;

using Sprout.Exam.Business.CustomValueResolver;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Employee, EmployeeDto>()
                 .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                 .ForMember(dest => dest.Birthdate, opt => opt.MapFrom<BirthdateValueResolver>());

            CreateMap<EmployeeDto, Employee>()
                 .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.TypeId));
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.TypeId)); 
            CreateMap<EditEmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.TypeId)); 
        }
    }
}
