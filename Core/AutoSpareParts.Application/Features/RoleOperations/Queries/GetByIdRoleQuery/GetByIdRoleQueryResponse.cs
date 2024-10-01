using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;

public class GetByIdRoleQueryResponse
{
    public IDataResult<RoleDto> Result { get; set; }
}