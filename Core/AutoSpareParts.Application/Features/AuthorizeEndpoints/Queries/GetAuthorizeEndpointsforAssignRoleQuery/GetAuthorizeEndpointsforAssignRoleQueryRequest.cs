using AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;
using MediatR;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;

public class GetAuthorizeEndpointsforAssignRoleQueryRequest:IRequest<GetAuthorizeEndpointsforAssignRoleQueryResponse>
{
    public string Query { get; set; }
}