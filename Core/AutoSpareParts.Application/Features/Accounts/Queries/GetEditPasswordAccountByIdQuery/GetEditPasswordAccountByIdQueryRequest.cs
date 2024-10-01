using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;

public class GetEditPasswordAccountByIdQueryRequest:IRequest<GetEditPasswordAccountByIdQueryResponse>
{
    public string Id { get; set; }
}