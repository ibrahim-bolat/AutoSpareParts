using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAdressListByEndpointQuery;

public class GetIPAdressListByEndpointQueryResponse
{
    public IDataResult<HashSet<AssignIPAddressDto>> Result { get; set; }
}