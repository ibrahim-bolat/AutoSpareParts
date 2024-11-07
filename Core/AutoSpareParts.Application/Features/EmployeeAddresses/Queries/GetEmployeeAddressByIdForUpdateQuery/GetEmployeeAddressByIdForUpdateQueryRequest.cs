using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByIdForUpdateQuery;

public class GetByIdUpdateEmployeeAddressQueryRequest:IRequest<GetByIdUpdateEmployeeAddressQueryResponse>
{
    public int Id { get; set; }
}