using AutoSpareParts.Application.Features.AuthorizeEndpoints.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByEndpointQuery;

public class GetIpAdressesByEndpointQueryResponse
{
    public IDataResult<HashSet<IpAssignDto>> Result { get; set; }
}