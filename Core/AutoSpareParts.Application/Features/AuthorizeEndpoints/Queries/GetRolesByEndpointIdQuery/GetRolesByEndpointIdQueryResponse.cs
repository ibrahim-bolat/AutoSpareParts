
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetRolesByEndpointIdQuery;

public class GetRolesByEndpointIdQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}