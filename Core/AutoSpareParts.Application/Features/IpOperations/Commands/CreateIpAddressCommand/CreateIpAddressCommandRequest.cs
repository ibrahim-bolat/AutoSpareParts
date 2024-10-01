using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.IpOperations.Commands.CreateIpAddressCommand;

public class CreateIpAddressCommandRequest:IRequest<CreateIpAddressCommandResponse>
{
    public IpDto IpDto{ get; set; }
}