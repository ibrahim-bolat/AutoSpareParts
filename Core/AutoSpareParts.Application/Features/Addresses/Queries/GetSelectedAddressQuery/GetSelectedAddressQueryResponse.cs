
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetSelectedAddressQuery;

public class GetSelectedAddressQueryResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}