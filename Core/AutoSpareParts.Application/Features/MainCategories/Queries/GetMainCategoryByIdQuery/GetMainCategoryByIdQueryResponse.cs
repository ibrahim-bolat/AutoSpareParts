using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryByIdQuery;

public class GetMainCategoryByIdQueryResponse
{
    public IDataResult<MainCategoryListDto> Result { get; set; }
}