
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetByIdDetailAddressQuery;

public class GetByIdDetailAddressQueryResponse
{
    public IDataResult<DetailAddressDto> Result { get; set; }
}