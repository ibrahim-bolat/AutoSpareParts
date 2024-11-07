using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetActiveEmployeeListQuery;

public class GetActiveEmployeeListQueryRequest:IRequest<GetActiveEmployeeListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}