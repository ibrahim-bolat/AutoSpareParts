using AutoSpareParts.Application.Wrappers.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetDistrictListByCityIdQuery;

public class GetDistrictListByCityIdQueryResponse
{
    public DataResult<List<SelectListItem>> Result { get; set; }
}