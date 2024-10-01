using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetByIdDetailAddressQuery;

public class GetByIdDetailAddressQueryRequest:IRequest<GetByIdDetailAddressQueryResponse>
{
    public int Id { get; set; }
}