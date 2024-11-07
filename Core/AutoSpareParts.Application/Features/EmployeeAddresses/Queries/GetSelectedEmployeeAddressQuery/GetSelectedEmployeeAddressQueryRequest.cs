using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetSelectedAddressQuery;

public class GetSelectedEmployeeAddressQueryRequest:IRequest<GetSelectedEmployeeAddressQueryResponse>
{
    public EmployeeAddressDto EmployeeAddressDto { get; set; }
}