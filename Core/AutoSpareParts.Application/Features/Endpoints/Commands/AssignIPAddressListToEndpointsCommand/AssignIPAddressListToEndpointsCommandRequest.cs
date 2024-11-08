using MediatR;

namespace AutoSpareParts.Application.Features.Endpoints.Commands.AssignIPAddressListToEndpointsCommand;

public class AssignIPAddressListToEndpointsCommandRequest:IRequest<AssignIPAddressListToEndpointsCommandResponse>
{
    public string IPAreaName { get; set; }
    public string IPMenuName { get; set; }
    public int EndpointId { get; set; }
    public List<int> IPIds { get; set; }
}