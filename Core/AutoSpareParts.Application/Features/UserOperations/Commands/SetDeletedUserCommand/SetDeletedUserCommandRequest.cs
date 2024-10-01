using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Commands.SetDeletedUserCommand;

public class SetDeletedUserCommandRequest:IRequest<SetDeletedUserCommandResponse>
{
    public string Id { get; set; }
}