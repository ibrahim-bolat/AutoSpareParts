
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetByIdUserRoleListQuery;

public class GetByIdUserRoleListQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}