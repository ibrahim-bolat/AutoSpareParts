using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.MappingProfiles;

public class EmployeeAddressProfile:Profile
{
    public EmployeeAddressProfile()
    {
        CreateMap<EmployeeAddress, EmployeeAddressDto>().ReverseMap();
        CreateMap<EmployeeAddress, EmployeeAddressDetailDto>().ReverseMap();
    }
}