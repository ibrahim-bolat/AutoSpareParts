using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.Employees.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.CreatIPAddressCommand;

public class CreateIPAddressCommandRequest:IRequest<CreateIPAddressCommandResponse>
{
    public IPDto IPDto{ get; set; }
}