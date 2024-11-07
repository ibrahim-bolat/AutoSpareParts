using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeByIdQuery;

public class GetEmployeeByIdQueryRequest : IRequest<GetEmployeeByIdQueryResponse>
{
    public string Id { get; set; }
}