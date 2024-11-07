using AutoMapper;
using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Application.Features.Employees.MappingProfiles;

public class EmployeeProfile:Profile
{
    public EmployeeProfile()
    {
        CreateMap<AppUser, CreateEmployeeDto>().ReverseMap();
        CreateMap<AppUser, EmployeeSummaryDto>().ReverseMap();
        CreateMap<AppUser, EditEmployeePasswordDto>().ReverseMap();
    }
}