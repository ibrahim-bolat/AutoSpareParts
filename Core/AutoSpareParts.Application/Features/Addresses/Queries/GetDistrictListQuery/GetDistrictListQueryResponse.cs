using AutoSpareParts.Application.Wrappers.Abstract;
using AutoSpareParts.Application.Wrappers.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetDistrictListQuery;

public class GetDistrictListQueryResponse
{
    public DataResult<List<SelectListItem>> Result { get; set; }
}