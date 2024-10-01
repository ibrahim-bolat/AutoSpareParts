using MediatR;

namespace AutoSpareParts.Application.Features.RoleOperations.Commands.SetRoleActiveCommand;

public class SetRoleActiveCommandRequest:IRequest<SetRoleActiveCommandResponse>
{
    public int Id { get; set; }
}