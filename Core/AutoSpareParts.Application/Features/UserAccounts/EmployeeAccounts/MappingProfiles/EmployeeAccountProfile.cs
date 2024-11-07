using AutoMapper;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.MappingProfiles;

public class EmployeeAccountProfile : Profile
{
    public EmployeeAccountProfile()
    {
        CreateMap<Employee, RegisterEmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeSummaryCardDto>().ForMember(dest => dest.DefaultAddressDetail
                , opt => opt.MapFrom(src => src.EmployeeAddresses.FirstOrDefault(x => x.DefaultAddress).AddressDetails))
            .ReverseMap();
        CreateMap<Employee, EditEmployeePasswordDto>().ReverseMap();
        CreateMap<EmployeeAddress, EmployeeAddressSummaryDto>()
            .ForMember(dest => dest.FullName
                , opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
            .ReverseMap();
    }
}