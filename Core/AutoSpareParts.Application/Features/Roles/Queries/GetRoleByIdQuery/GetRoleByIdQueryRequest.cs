using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleByIdQuery;

public class GetRoleByIdQueryRequest:IRequest<GetRoleByIdQueryResponse>
{
    public string Id { get; set; }
}