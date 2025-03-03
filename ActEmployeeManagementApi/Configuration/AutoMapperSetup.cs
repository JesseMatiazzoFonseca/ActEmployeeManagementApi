using AutoMapper;
using Domain.Entities;
using Domain.Models.Request;

namespace API.Configuration
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<EmployeeRequest, Employee>().ReverseMap();
            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.SenhaCripto, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
        }
    }
}
