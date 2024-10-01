using AutoSpareParts.Application.Features.Addresses.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Commands.CreateAddressCommand;

public class CreateAddressCommandRequest:IRequest<CreateAddressCommandResponse>
{
    public AddressDto AddressDto { get; set; }
}