using MediatR;

namespace AutoSpareParts.Application.Features.IpOperations.Commands.SetIpAddressPassiveCommand;

public class SetIpAddressPassiveCommandRequest:IRequest<SetIpAddressPassiveCommandResponse>
{
    public int Id { get; set; }
}