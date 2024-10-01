using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByIdForUserSummaryCardQuery;

public class GetByIdForUserSummaryCardQueryRequest:IRequest<GetByIdForUserSummaryCardQueryResponse>
{
    public int Id { get; set; }
}