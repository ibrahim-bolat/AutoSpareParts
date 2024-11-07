using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEditEmployeePasswordByIdQuery;

public class GetEditEmployeePasswordByIdQueryRequest : IRequest<GetEditEmployeePasswordByIdQueryResponse>
{
    public string Id { get; set; }
}