
using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByIdForUserSummaryCardQuery;

public class GetByIdForUserSummaryCardQueryResponse
{
    public IDataResult<UserSummaryCardDto> Result { get; set; }
}