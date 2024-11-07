using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleByIdQuery;

public class GetRoleByIdQueryResponse
{
    public IDataResult<RoleDto> Result { get; set; }
}