using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.SetPassiveIPAddressCommand;

public class SetPassiveIpAddressCommandRequest:IRequest<SetPassiveIpAddressCommandResponse>
{
    public int Id { get; set; }
}