using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IPAddresses.Commands.CreatIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.Commands.SetActiveIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.Commands.SetPassiveIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.Commands.UpdateIPAddressCommand;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressByIdQuery;
using AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Constants;


namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class IPAddressController : Controller
{
    private readonly IMediator _mediator;

    public IPAddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetIPAddressList(DatatableRequestDto datatableRequestDto)
    {
        var dresult = await _mediator.Send(new GetIPAddressListQueryRequest()
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
    [Endpoint(Menu = MenuDefinition.IPAddress, EndpointType = EndpointType.Writing, Decription = EndpointDecription.PostCreateIPAddress)]
    public async Task<IActionResult> CreateIPAddress(IPDto IPDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_CreateIPAddressModalPartial", IPDto);
        }

        var dresult = await _mediator.Send(new CreateIPAddressCommandRequest()
        {
            IPDto = IPDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }


    [HttpPost]
    [Endpoint(Menu = MenuDefinition.IPAddress, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostUpdateIPAddress)]
    public async Task<IActionResult> UpdateIPAddress(IPDto IPDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_UpdateIPAddressModalPartial", IPDto);
        }

        var dresult = await _mediator.Send(new UpdateIPAddressCommandRequest()
        {
            IPDto = IPDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError(nameof(Messages.IPNotFound), Messages.IPNotFound);
            return PartialView("PartialViews/_UpdateIPAddressModalPartial", IPDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetActiveIPAddress(int id)
    {
        var dresult = await _mediator.Send(new SetActiveIPAddressCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError(nameof(Messages.IPNotFound), Messages.IPNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPActive))
        {
            ModelState.AddModelError(nameof(Messages.IPActive), Messages.IPActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetPassiveIPAddress(int id)
    {
        var dresult = await _mediator.Send(new SetPassiveIPAddressCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError(nameof(Messages.IPNotFound), Messages.IPNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotActive))
        {
            ModelState.AddModelError(nameof(Messages.IPNotActive), Messages.IPNotActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.IPAddress, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetIPAddressById)]
    public async Task<IActionResult> GetIPAddressById(int id)
    {
        var dresult = await _mediator.Send(new GetIPAddressByIdQueryRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, ip = dresult.Result.Data });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IPNotFound))
        {
            ModelState.AddModelError(nameof(Messages.IPNotFound), Messages.IPNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }
}