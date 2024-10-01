using AutoSpareParts.Application.Wrappers.Abstract;
using AutoSpareParts.Application.Features.UserOperations.DTOs;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;

public class GetByIdForUserSummaryQueryResponse
{
    public IDataResult<UserSummaryDto> Result { get; set; }
}