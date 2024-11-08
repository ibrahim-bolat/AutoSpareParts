using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListByEndpointQuery;

public class GetIPAddressListByEndpointQueryResponse
{
    public IDataResult<HashSet<AssignIPAddressDto>> Result { get; set; }
}