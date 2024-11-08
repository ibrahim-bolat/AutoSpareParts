using AutoSpareParts.Application.Features.Endpoints.Commands.AssignIPAddressListToEndpointsCommand;
using AutoSpareParts.Application.Features.Endpoints.Commands.AssignRoleListToEndpointsCommand;
using AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignIPAddressQuery;
using AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;
using AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListByEndpointQuery;
using AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEndpointIdQuery;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.MVC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EndpointController : Controller
{

    private readonly IMediator _mediator;
    private readonly string IndexAction = "Index";

    public EndpointController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetEndpointListForAssignRole(string query)
    {

        var dresult = await _mediator.Send(new GetEndpointListForAssignRoleQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoleListToEndpoints(int id, List<int> roleIds)
    {
        if (id > 0 && roleIds != null)
        {
            var dresult = await _mediator.Send(new AssignRoleListToEndpointsCommandRequest()
            {
                Id = id,
                RoleIds = roleIds
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
        }
        return Json(new { success = false });
    }

    [HttpGet]
    public async Task<IActionResult> GetRoleListByEndpointId(string id)
    {
        var dresult = await _mediator.Send(new GetRoleListByEndpointIdQueryRequest()
        {
            Id = id,
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, roles = dresult.Result.Data });
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }

    [HttpGet]
    public IActionResult Endpoints()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetEndpointListForAssignIPAddress(string query)
    {

        var dresult = await _mediator.Send(new GetEndpointListForAssignIPAddressQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AssignIPAddressListToEndpoints(string IPAreaName, string IPMenuName, int IPEndpointId, List<int> IPIds)
    {
        if (IPIds is not null)
        {
            var dresult = await _mediator.Send(new AssignIPAddressListToEndpointsCommandRequest()
            {
                IPAreaName = IPAreaName,
                IPMenuName = IPMenuName,
                EndpointId = IPEndpointId,
                IPIds = IPIds,
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
        }
        return Json(new { success = false });
    }
    [HttpPost]
    public async Task<IActionResult> GetIPAddressListByEndpoint(string areaName, string menuName, int endpointId)
    {
        var dresult = await _mediator.Send(new GetIPAddressListByEndpointQueryRequest()
        {
            AreaName = areaName,
            MenuName = menuName,
            EndpointId = endpointId,
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return PartialView("PartialViews/_EndpointIPModalPartial", dresult.Result.Data);
        }

        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }
}