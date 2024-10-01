using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Commands.AssignUserRoleListCommand;

public class AssignUserRoleListCommandRequest:IRequest<AssignUserRoleListCommandResponse>
{
    public string Id { get; set; }
    public List<int> RoleIds { get; set; }
}