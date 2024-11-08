using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEditEmployeeAccountPasswordByIdQuery;

public class GetEditEmployeeAccountPasswordByIdQueryResponse
{
    public IDataResult<EditEmployeeAccountPasswordDto> Result { get; set; }
}