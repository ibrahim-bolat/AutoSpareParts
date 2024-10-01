using AutoSpareParts.Application.Features.Addresses.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Commands.UpdateAddressCommand;

public class UpdateAddressCommandRequest:IRequest<UpdateAddressCommandResponse>
{
    public AddressDto AddressDto { get; set; }
    public string ModifiedByName { get; set; }
}