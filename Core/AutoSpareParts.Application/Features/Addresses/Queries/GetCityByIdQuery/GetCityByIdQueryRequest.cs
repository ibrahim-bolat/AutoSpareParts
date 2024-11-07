using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCityByIdQuery;

public class GetCityByIdQueryRequest : IRequest<GetCityByIdQueryResponse>
{
    public int Id { get; set; }
}