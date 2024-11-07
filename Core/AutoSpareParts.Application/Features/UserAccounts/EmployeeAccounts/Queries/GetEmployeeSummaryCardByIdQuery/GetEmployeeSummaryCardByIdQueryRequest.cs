using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeSummaryCardByIdQuery;

public class GetByIdForUserSummaryCardQueryRequest : IRequest<GetEmployeeSummaryCardByIdQueryResponse>
{
    public int Id { get; set; }
}