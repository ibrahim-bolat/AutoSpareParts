using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;

public record EmployeeDetailDto : BaseDto
{
    public EmployeeDto EmployeeDto { get; init; }
    public List<EmployeeAddressSummaryDto> EmployeeAddressSummaryDtos { get; init; }
}
