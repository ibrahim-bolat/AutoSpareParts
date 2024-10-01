
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCreateAddressQuery;

public class GetCreateAddressQueryResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}