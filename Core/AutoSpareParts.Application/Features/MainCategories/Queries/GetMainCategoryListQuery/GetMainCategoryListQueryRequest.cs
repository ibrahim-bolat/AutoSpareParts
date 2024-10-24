using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryListQuery;

public class GetMainCategoryListQueryRequest : IRequest<GetMainCategoryListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}