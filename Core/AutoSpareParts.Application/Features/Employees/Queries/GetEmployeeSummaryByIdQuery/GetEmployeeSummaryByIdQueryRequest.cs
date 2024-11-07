using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryByIdQuery;

public class GetEmployeeSummaryByIdQueryRequest:IRequest<GetEmployeeSummaryByIdQueryResponse>
{
    public string Id { get; set; }
}