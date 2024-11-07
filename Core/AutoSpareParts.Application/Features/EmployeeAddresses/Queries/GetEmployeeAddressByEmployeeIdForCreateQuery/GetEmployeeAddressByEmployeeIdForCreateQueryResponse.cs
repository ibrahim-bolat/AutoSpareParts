
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByEmployeeIdForCreateQuery;

public class GetEmployeeAddressByEmployeeIdForCreateQueryResponse
{
    public IDataResult<EmployeeAddressDto> Result { get; set; }
}