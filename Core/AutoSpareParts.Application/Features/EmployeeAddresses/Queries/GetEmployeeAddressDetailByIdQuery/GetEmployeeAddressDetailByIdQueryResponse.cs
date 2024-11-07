using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmpLoyeeAddresses.Queries.GetEmployeeAddressDetailByIdQuery;

public class GetEmployeeAddressDetailByIdQueryResponse
{
    public IDataResult<EmployeeAddressDetailDto> Result { get; set; }
}