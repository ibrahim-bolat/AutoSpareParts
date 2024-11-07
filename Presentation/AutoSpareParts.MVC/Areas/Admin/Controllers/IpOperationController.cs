using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IPAddresses.Commands.CreatIPpAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.Commands.SetActiveIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.Commands.SetPassiveIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.Commands.UpdateIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressByIdQuery;
using AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Messages = AutoSpareParts.Application.Features.IPAddresses.Constants.Messages;


namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class IpOperationController : Controller
{
    private readonly IMediator _mediator;

    public IpOperationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllIpAddresses(DatatableRequestDto datatableRequestDto)
    {
        var dresult = await _mediator.Send(new GetIpAddressListQueryRequest()
        {
            DatatableRequestDto = datatableRequestDto
        });

        var jsonData = new
        {
            draw = dresult.Result.Data.Draw, recordsFiltered = dresult.Result.Data.RecordsFiltered,
            recordsTotal = dresult.Result.Data.RecordsTotal, data = dresult.Result.Data.Data, isSusccess = true
        };
        return Ok(jsonData);
    }


    [HttpPost]
    [Endpoint(Menu = MenuDecription.IPAddress, EndpointType = EndpointType.Writing,
        Definition = "Create IPAddress")]
    public async Task<IActionResult> CreateIpAddress(IPDto ipDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_IpCreateModalPartial", ipDto);
        }

        var dresult = await _mediator.Send(new CreateIpAddressCommandRequest()
        {
            IpDto = ipDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }


    [HttpPost]
    [Endpoint(Menu = MenuDecription.IPAddress, EndpointType = EndpointType.Updating,
        Definition = "Update IPAddress")]
    public async Task<IActionResult> UpdateIpAddress(IPDto ipDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_IpUpdateModalPartial", ipDto);
        }

        var dresult = await _mediator.Send(new UpdateIpAddressCommandRequest()
        {
            IpDto = ipDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError("IPNotFound", Messages.IPNotFound);
            return PartialView("PartialViews/_IpUpdateModalPartial", ipDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetIpAddressActive(int id)
    {
        var dresult = await _mediator.Send(new SetActiveIpAddressCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError("IPNotFound", Messages.IPNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IPActive))
        {
            ModelState.AddModelError("IPActive", Messages.IPActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetIpAddressPassive(int id)
    {
        var dresult = await _mediator.Send(new SetPassiveIpAddressCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError("IPNotFound", Messages.IPNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IPNotActive))
        {
            ModelState.AddModelError("IPNotActive", Messages.IPNotActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.IPAddress, EndpointType = EndpointType.Reading,
        Definition = "Get By EmloyeeId IPAddress Details")]
    public async Task<IActionResult> GetIpAddressById(int id)
    {
        var dresult = await _mediator.Send(new GetIpAddressByIdQueryRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, ip = dresult.Result.Data });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError("IPNotFound", Messages.IPNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }
}