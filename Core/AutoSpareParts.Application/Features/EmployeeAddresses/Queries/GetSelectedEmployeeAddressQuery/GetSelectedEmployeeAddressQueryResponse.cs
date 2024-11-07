
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetSelectedAddressQuery;

public class GetSelectedEmployeeAddressQueryResponse
{
    public IDataResult<EmployeeAddressDto> Result { get; set; }
}