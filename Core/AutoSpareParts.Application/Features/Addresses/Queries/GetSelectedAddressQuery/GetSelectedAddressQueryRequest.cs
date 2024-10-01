using AutoSpareParts.Application.Features.Addresses.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetSelectedAddressQuery;

public class GetSelectedAddressQueryRequest:IRequest<GetSelectedAddressQueryResponse>
{
    public AddressDto AddressDto { get; set; }
}