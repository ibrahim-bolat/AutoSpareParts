using MediatR;

namespace AutoSpareParts.Application.Features.IpOperations.Queries.GetByIdIpAddressQuery;

public class GetByIdIpAddressQueryRequest:IRequest<GetByIdIpAddressQueryResponse>
{
    public int Id { get; set; }
}