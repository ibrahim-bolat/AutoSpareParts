using AutoSpareParts.Application.Wrappers.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCityByIdQuery;

public class GetCityByIdQueryResponse
{
    public DataResult<SelectListItem> Result { get; set; }
}