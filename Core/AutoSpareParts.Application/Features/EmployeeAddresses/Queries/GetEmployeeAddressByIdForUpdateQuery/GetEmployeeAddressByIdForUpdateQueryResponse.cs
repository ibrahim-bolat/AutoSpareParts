
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByIdForUpdateQuery;

public class GetByIdUpdateEmployeeAddressQueryResponse
{
    public IDataResult<EmployeeAddressDto> Result { get; set; }
}