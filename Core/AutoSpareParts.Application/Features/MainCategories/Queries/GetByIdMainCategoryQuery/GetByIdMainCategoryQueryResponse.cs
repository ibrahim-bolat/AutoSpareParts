using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetByIdMainCategoryQuery;

public class GetByIdMainCategoryQueryResponse
{
    public IDataResult<MainCategoryListDto> Result { get; set; }
}