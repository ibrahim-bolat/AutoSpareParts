using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetByIdMainCategoryQuery;

public class GetByIdMainCategoryQueryRequest:IRequest<GetByIdMainCategoryQueryResponse>
{
    public int Id { get; set; }
}