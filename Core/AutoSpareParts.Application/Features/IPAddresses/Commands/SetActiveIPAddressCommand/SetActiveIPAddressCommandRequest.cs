using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.SetActiveIPAddressCommand;

public class SetActiveIpAddressCommandRequest:IRequest<SetActiveIpAddressCommandResponse>
{
    public int Id { get; set; }
}