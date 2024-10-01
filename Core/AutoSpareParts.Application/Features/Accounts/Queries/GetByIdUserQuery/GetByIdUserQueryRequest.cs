using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByIdUserQuery;

public class GetByIdUserQueryRequest:IRequest<GetByIdUserQueryResponse>
{
    public string Id { get; set; }
}