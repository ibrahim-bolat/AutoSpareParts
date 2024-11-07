using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetDistrictListByCityIdQuery;

public class GetDistrictListByCityIdQueryRequest : IRequest<GetDistrictListByCityIdQueryResponse>
{
    public int CityId { get; set; }
}