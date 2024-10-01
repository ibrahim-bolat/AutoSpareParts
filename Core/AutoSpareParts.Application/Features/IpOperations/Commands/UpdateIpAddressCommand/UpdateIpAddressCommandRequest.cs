using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.IpOperations.Commands.UpdateIpAddressCommand;

public class UpdateIpAddressCommandRequest:IRequest<UpdateIpAddressCommandResponse>
{
    public IpDto IpDto{ get; set; }
}