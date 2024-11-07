using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.Employees.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.CreatIPpAddressCommand;

public class CreateIpAddressCommandRequest:IRequest<CreateIpAddressCommandResponse>
{
    public IPDto IpDto{ get; set; }
}