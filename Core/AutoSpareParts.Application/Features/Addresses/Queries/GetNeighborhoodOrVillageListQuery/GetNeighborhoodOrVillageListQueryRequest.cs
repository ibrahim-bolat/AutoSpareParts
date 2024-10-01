using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetNeighborhoodOrVillageListQuery;

public class GetNeighborhoodOrVillageListQueryRequest:IRequest<GetNeighborhoodOrVillageListQueryResponse>
{
    public int DistrictId { get; set; }
}