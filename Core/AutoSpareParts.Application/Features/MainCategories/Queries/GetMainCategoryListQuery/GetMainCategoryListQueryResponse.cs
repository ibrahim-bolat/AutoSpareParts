using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryListQuery;

public class GetMainCategoryListQueryResponse
{
    public IDataResult<DatatableResponseDto<MainCategoryListDto>> Result { get; set; }
}