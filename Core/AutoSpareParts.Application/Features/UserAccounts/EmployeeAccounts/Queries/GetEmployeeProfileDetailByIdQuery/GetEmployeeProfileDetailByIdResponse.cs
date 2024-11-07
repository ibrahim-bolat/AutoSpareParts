using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeProfileDetailByIdQuery;

public class GetEmployeeProfileDetailByIdResponse
{
    public IDataResult<EmployeeDetailDto> Result { get; set; }
}