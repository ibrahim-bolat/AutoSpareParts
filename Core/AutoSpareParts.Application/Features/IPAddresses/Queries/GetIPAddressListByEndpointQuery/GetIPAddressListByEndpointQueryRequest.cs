using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListByEndpointQuery;

public class GetIPAddressListByEndpointQueryRequest : IRequest<GetIPAddressListByEndpointQueryResponse>
{
    public string AreaName { get; set; }
    public string MenuName { get; set; }
    public int EndpointId { get; set; }
}