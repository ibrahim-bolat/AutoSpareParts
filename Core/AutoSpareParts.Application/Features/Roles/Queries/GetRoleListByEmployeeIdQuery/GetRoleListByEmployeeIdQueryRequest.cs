using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEmployeeIdQuery;

public class GetRoleListByEmployeeIdQueryRequest : IRequest<GetRoleListByEmployeeIdQueryResponse>
{
    public string EmployeeId { get; set; }
}