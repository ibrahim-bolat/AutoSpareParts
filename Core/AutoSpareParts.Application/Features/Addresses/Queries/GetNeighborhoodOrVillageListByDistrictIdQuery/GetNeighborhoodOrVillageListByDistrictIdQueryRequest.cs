using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetNeighborhoodOrVillageListByDistrictIdQuery;

public class GetNeighborhoodOrVillageListByDistrictIdQueryRequest : IRequest<GetNeighborhoodOrVillageListByDistrictIdQueryResponse>
{
    public int DistrictId { get; set; }
}