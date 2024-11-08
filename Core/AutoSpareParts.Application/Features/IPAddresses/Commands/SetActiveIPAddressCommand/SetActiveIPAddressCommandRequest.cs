using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.SetActiveIPAddressCommand;

public class SetActiveIPAddressCommandRequest:IRequest<SetActiveIPAddressCommandResponse>
{
    public int Id { get; set; }
}