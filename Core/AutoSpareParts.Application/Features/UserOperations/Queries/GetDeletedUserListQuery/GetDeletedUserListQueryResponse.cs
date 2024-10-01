
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryResponse
{
    public IDataResult<DatatableResponseDto<UserSummaryDto>> Result { get; set; }
}