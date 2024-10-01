using MediatR;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.Queries.GetRolesByEndpointIdQuery;

public class GetRolesByEndpointIdQueryRequest:IRequest<GetRolesByEndpointIdQueryResponse>
{
    public string Id { get; set; }
}