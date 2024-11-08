using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressByIdQuery;

public class GetIPAddressByIdQueryRequest:IRequest<GetIPAddressByIdQueryResponse>
{
    public int Id { get; set; }
}