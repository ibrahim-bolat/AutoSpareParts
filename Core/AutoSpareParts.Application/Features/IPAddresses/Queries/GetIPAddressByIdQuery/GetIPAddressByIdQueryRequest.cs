using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressByIdQuery;

public class GetIpAddressByIdQueryRequest:IRequest<GetIpAddressByIdQueryResponse>
{
    public int Id { get; set; }
}