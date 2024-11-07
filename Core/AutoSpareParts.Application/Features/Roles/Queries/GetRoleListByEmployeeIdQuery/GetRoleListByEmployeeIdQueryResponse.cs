
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEmployeeIdQuery;

public class GetRoleListByEmployeeIdQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}