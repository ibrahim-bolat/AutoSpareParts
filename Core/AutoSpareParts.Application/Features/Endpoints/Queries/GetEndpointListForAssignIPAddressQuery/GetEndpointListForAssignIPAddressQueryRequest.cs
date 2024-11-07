using AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;
using MediatR;

namespace AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignIPAddressQuery;

public class GetEndpointListForAssignIPAddressQueryRequest:IRequest<GetEndpointListForAssignIPAddressQueryResponse>
{
    public string Query { get; set; }
}