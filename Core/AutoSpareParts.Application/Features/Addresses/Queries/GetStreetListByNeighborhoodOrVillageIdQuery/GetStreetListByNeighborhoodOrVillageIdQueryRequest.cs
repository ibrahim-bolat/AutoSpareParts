using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetStreetListByNeighborhoodOrVillageIdQuery;

public class GetStreetListByNeighborhoodOrVillageIdQueryRequest : IRequest<GetStreetListByNeighborhoodOrVillageIdQueryResponse>
{
    public int NeighborhoodOrVillageId { get; set; }
}