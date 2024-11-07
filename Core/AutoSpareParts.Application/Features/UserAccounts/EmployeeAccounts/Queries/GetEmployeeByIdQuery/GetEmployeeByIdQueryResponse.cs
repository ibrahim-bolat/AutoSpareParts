using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeByIdQuery;

public class GetEmployeeByIdQueryResponse
{
    public IDataResult<EmployeeDto> Result { get; set; }
}