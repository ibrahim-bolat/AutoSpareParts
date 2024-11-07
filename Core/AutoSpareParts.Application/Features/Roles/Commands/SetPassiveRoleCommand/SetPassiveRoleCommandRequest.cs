using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Commands.SetPassiveRoleCommand;

public class SetPassiveRoleCommandRequest:IRequest<SetPassiveRoleCommandResponse>
{
    public int Id { get; set; }
}