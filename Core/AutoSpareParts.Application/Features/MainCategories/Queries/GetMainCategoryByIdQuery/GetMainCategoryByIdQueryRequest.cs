using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryByIdQuery;

public class GetMainCategoryByIdQueryRequest:IRequest<GetMainCategoryByIdQueryResponse>
{
    public int Id { get; set; }
}