using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.RoleOperations.Queries.GetUsersOfTheRoleQuery;

public class GetUsersOfTheRoleQueryResponse
{
    public IDataResult<DatatableResponseDto<UserSummaryDto>> Result { get; set; }
}