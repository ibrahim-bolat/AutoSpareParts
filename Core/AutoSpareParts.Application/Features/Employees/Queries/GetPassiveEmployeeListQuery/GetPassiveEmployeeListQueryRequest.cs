using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetPassiveEmployeeListQuery;

public class GetPassiveEmployeeListQueryRequest:IRequest<GetPassiveEmployeeListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}