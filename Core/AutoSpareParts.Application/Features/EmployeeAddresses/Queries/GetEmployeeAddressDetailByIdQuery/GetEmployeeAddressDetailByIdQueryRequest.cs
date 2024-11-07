using AutoSpareParts.Application.Features.EmpLoyeeAddresses.Queries.GetEmployeeAddressDetailByIdQuery;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetByIdDetailEmployeeAddressQuery;

public class GetEmployeeAddressDetailByIdQueryRequest:IRequest<GetEmployeeAddressDetailByIdQueryResponse>
{
    public int Id { get; set; }
}