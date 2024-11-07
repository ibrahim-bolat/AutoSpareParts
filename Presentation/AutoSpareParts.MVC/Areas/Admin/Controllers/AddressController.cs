using AutoSpareParts.Application.Features.Addresses.Queries.GetDistrictListByCityIdQuery;
using AutoSpareParts.Application.Features.Addresses.Queries.GetNeighborhoodOrVillageListByDistrictIdQuery;
using AutoSpareParts.Application.Features.Addresses.Queries.GetStreetListByNeighborhoodOrVillageIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AddressController : Controller
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetDistrictListByCityId(int cityId)
    {
        var dresult = await _mediator.Send(new GetDistrictListByCityIdQueryRequest()
        {
            CityId = cityId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, districts = dresult.Result.Data});
        }
        return Json(new { success = false });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetNeighborhoodOrVillageListByDistrictId(int districtId)
    {
        var dresult = await _mediator.Send(new GetNeighborhoodOrVillageListByDistrictIdQueryRequest()
        {
            DistrictId = districtId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, neighborhoodsOrVillages = dresult.Result.Data});
        }
        return Json(new { success = false });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetStreetListByNeighborhoodOrVillageId(int neighborhoodOrVillageId)
    {
        var dresult = await _mediator.Send(new GetStreetListByNeighborhoodOrVillageIdQueryRequest()
        {
            NeighborhoodOrVillageId = neighborhoodOrVillageId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, streets = dresult.Result.Data});
        }
        return Json(new { success = false });
    }
}