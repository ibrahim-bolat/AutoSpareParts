using AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;
using MediatR;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignIpQuery;

public class GetAuthorizeEndpointsforAssignIpQueryRequest:IRequest<GetAuthorizeEndpointsforAssignIpQueryResponse>
{
    public string Query { get; set; }
}