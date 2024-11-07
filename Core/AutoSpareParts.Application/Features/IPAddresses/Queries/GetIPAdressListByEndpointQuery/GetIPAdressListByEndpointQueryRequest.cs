using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAdressListByEndpointQuery;

public class GetIPAdressListByEndpointQueryRequest : IRequest<GetIPAdressListByEndpointQueryResponse>
{
    public string AreaName { get; set; }
    public string MenuName { get; set; }
    public int EndpointId { get; set; }
}