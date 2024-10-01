using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetByIdUserRoleListQuery;

public class GetByIdUserRoleListQueryRequest:IRequest<GetByIdUserRoleListQueryResponse>
{
    public string Id { get; set; }
}