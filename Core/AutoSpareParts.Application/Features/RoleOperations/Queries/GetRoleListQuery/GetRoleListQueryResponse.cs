using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.RoleOperations.Queries.GetRoleListQuery;

public class GetRoleListQueryResponse
{
    public IDataResult<DatatableResponseDto<RoleDto>> Result { get; set; }
}