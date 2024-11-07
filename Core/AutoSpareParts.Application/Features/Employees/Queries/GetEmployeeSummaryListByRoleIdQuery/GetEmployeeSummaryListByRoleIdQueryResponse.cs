using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryListByRoleIdQuery;

public class GetEmployeeSummaryListByRoleIdQueryResponse
{
    public IDataResult<DatatableResponseDto<EmployeeSummaryDto>> Result { get; set; }
}