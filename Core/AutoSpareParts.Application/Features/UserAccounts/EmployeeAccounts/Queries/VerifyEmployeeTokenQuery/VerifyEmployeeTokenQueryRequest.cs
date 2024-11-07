using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.VerifyEmployeeTokenQuery;

public class VerifyEmployeeTokenQueryRequest : IRequest<VerifyEmployeeTokenQueryResponse>
{
    public string EmployeeId { get; set; }
    public string Token { get; set; }
}