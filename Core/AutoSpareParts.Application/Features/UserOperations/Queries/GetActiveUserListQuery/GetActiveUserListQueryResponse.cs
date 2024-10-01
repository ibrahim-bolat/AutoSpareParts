using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetActiveUserListQuery;

public class GetActiveUserListQueryResponse
{
    public IDataResult<DatatableResponseDto<UserSummaryDto>> Result { get; set; }
}