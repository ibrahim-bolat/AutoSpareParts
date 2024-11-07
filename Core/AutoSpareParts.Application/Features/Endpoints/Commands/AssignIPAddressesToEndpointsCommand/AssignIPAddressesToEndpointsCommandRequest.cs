using MediatR;

namespace AutoSpareParts.Application.Features.Endpoints.Commands.AssignIPAddressesToEndpointsCommand;

public class AssignIpAddressesToEndpointsCommandRequest:IRequest<AssignIpAddressesToEndpointsCommandResponse>
{
    public string IPAreaName { get; set; }
    public string IPMenuName { get; set; }
    public int EndpointId { get; set; }
    public List<int> IPIds { get; set; }
}