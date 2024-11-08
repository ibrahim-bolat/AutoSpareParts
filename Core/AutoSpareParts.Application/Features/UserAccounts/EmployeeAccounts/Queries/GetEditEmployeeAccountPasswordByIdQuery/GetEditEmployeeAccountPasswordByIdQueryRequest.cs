using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEditEmployeeAccountPasswordByIdQuery;

public class GetEditEmployeeAccountPasswordByIdQueryRequest : IRequest<GetEditEmployeeAccountPasswordByIdQueryResponse>
{
    public string Id { get; set; }
}