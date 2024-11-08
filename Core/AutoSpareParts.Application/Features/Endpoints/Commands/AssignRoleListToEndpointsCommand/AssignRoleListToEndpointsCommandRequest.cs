using MediatR;

namespace AutoSpareParts.Application.Features.Endpoints.Commands.AssignRoleListToEndpointsCommand;

public class AssignRoleListToEndpointsCommandRequest:IRequest<AssignRoleListToEndpointsCommandResponse>
{
    public int Id { get; set; }
    public List<int> RoleIds { get; set; }
}