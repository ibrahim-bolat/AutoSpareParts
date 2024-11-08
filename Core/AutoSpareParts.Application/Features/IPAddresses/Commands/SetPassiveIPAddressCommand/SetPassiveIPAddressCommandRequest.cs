using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.SetPassiveIPAddressCommand;

public class SetPassiveIPAddressCommandRequest:IRequest<SetPassiveIPAddressCommandResponse>
{
    public int Id { get; set; }
}