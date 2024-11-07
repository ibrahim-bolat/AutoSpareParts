using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeSummaryCardByIdQuery;

public class GetEmployeeSummaryCardByIdQueryResponse
{
    public IDataResult<EmployeeSummaryCardDto> Result { get; set; }
}