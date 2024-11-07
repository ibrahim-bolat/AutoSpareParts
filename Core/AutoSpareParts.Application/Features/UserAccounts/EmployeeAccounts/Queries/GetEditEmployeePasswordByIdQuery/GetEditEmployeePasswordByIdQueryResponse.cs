using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEditEmployeePasswordByIdQuery;

public class GetEditEmployeePasswordByIdQueryResponse
{
    public IDataResult<EditEmployeePasswordDto> Result { get; set; }
}