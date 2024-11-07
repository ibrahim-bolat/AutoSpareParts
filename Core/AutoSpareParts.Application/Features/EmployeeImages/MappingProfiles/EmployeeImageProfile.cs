using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.EmployeeImages.MappingProfiles;

public class EmployeeImageProfile:Profile
{
    public EmployeeImageProfile()
    {
        CreateMap<EmployeeImage, EmployeeImageDto>().ReverseMap();
        CreateMap<EmployeeImage, CreateEmployeeImageDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
        CreateMap<EmployeeImageDto, CreateEmployeeImageDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
    }
}