using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEndpointIdQuery;

public class GetRoleListByEndpointIdQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}