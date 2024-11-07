using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Commands.SetActiveRoleCommand;

public class SetActiveRoleCommandRequest:IRequest<SetActiveRoleCommandResponse>
{
    public int Id { get; set; }
}