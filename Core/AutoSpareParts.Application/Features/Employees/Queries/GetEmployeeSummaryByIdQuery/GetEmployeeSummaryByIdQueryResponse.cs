using AutoSpareParts.Application.Wrappers.Abstract;
using AutoSpareParts.Application.Features.Employees.DTOs;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryByIdQuery;

public class GetEmployeeSummaryByIdQueryResponse
{
    public IDataResult<EmployeeSummaryDto> Result { get; set; }
}