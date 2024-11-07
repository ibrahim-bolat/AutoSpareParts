
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetPassiveEmployeeListQuery;

public class GetPassiveEmployeeListQueryResponse
{
    public IDataResult<DatatableResponseDto<EmployeeSummaryDto>> Result { get; set; }
}