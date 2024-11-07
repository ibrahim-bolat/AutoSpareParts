using AutoSpareParts.Application.Features.Endpoints.Commands.AssignIPAddressesToEndpointsCommand;
using AutoSpareParts.Application.Features.Endpoints.Commands.AssignRolesToEndpointsCommand;
using AutoSpareParts.Application.Features.Endpoints.DTOs;
using AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignIPAddressQuery;
using AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;
using AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAdressListByEndpointQuery;
using AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEndpointIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EndpointController : Controller
{
    
    private readonly IMediator _mediator;

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
    public  async Task<IActionResult>  GetAuthorizeEndpointsforAssignRole(string query)
    {
         
        var dresult = await _mediator.Send(new GetEndpointListForAssignRoleQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }

    [HttpPost]
    public async Task<IActionResult>  AssignRolesByEndpointId(int id, List<int> roleIds)
    {
        if (id>0 && roleIds != null)
        {
            var dresult = await _mediator.Send(new AssignRolesToEndpointsCommandRequest()
            {
                Id=id,
                RoleIds = roleIds
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
        }
        return Json(new { success = false });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRolesByEndpointId(string id)
    {
        var dresult = await _mediator.Send(new GetRoleListByEndpointIdQueryRequest()
        {
            Id=id,
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, roles = dresult.Result.Data });
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
    
    [HttpGet]
    public IActionResult AuthorizeEndpoints()
    {
        return View();
    }
    [HttpGet]
    public  async Task<IActionResult>  GetAllAuthorizeEndpointsforAssignIp(string query)
    {
         
        var dresult = await _mediator.Send(new GetEndpointListForAssignIPAddressQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }
    
    [HttpPost]
    public async Task<IActionResult>  AssignIpAddresses(string ipAreaName,string ipMenuName ,int ipEndpointId, List<int> ipIds)
    {
        if (ipIds is not null)
        {
            var dresult = await _mediator.Send(new AssignIpAddressesToEndpointsCommandRequest()
            {
                IPAreaName=ipAreaName,
                IPMenuName=ipMenuName,
                EndpointId = ipEndpointId,
                IPIds=ipIds,
            });
   
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
        }
        return Json(new { success = false });
    }
    [HttpPost]
    public async Task<IActionResult> GetAllIpAdressesByEndpoint(string areaName,string menuName,int endpointId)
    {
        var dresult = await _mediator.Send(new GetIPAdressListByEndpointQueryRequest()
        {
            AreaName =areaName,
            MenuName =menuName,
            EndpointId=endpointId,
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return PartialView("PartialViews/_EndpointIpModalPartial",dresult.Result.Data);
        }
        
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
}