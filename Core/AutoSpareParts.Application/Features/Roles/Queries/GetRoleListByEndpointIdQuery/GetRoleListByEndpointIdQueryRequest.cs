using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEndpointIdQuery;

public class GetRoleListByEndpointIdQueryRequest : IRequest<GetRoleListByEndpointIdQueryResponse>
{
    public string Id { get; set; }
}