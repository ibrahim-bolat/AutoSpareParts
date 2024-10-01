using MediatR;

namespace AutoSpareParts.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;

public class GetByIdRoleQueryRequest:IRequest<GetByIdRoleQueryResponse>
{
    public string Id { get; set; }
}