
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Addresses.Commands.DeleteAddressCommand;

public class DeleteAddressCommandResponse
{
    public IDataResult<DetailAddressDto> Result { get; set; }
}