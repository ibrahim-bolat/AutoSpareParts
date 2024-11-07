using AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;
using MediatR;

namespace AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;

public class GetEndpointListForAssignRoleQueryRequest:IRequest<GetEndpointListForAssignRoleQueryResponse>
{
    public string Query { get; set; }
}