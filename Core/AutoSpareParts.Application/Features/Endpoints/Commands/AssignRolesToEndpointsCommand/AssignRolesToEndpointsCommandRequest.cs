using MediatR;

namespace AutoSpareParts.Application.Features.Endpoints.Commands.AssignRolesToEndpointsCommand;

public class AssignRolesToEndpointsCommandRequest:IRequest<AssignRolesToEndpointsCommandResponse>
{
    public int Id { get; set; }
    public List<int> RoleIds { get; set; }
}