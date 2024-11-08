using AutoSpareParts.Application.Features.Roles.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleByIdQuery;

public class GetRoleByIdQueryResponse
{
    public IDataResult<RoleDto> Result { get; set; }
}