using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.Employees.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.UpdateIPAddressCommand;

public class UpdateIpAddressCommandRequest:IRequest<UpdateIpAddressCommandResponse>
{
    public IPDto IpDto{ get; set; }
}