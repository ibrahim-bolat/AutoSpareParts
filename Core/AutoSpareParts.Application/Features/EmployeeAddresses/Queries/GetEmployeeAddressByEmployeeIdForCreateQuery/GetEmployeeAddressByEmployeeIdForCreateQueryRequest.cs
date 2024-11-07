using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByEmployeeIdForCreateQuery;

public class GetEmployeeAddressByEmployeeIdForCreateQueryRequest:IRequest<GetEmployeeAddressByEmployeeIdForCreateQueryResponse>
{
    public int EmployeeId { get; set; }
}