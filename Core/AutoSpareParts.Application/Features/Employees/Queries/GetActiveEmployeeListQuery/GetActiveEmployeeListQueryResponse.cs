using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetActiveEmployeeListQuery;

public class GetActiveEmployeeListQueryResponse
{
    public IDataResult<DatatableResponseDto<EmployeeSummaryDto>> Result { get; set; }
}