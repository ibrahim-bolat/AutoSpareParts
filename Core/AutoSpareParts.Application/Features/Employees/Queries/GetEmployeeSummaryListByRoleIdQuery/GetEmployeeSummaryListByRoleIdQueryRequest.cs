using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryListByRoleIdQuery;

public class GetEmployeeSummaryListByRoleIdQueryRequest : IRequest<GetEmployeeSummaryListByRoleIdQueryResponse>
{
    public string Id { get; set; }
    public DatatableRequestDto DatatableRequestDto { get; set; }
}