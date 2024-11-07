using AutoSpareParts.Application.Wrappers.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCityListQuery;

public class GetCityListQueryResponse
{
    public DataResult<List<SelectListItem>> Result { get; set; }
}