using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListQuery;

public class GetRoleListQueryResponse
{
    public IDataResult<DatatableResponseDto<RoleDto>> Result { get; set; }
}