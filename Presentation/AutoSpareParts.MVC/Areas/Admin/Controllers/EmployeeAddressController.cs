using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByIdForUpdateQuery;
using AutoSpareParts.Application.Features.EmployeeAddresses.Commands.CreateAddressCommand;
using AutoSpareParts.Application.Features.EmployeeAddresses.Commands.DeleteEmployeeAddressCommand;
using AutoSpareParts.Application.Features.EmployeeAddresses.Commands.UpdateEmployeeAddressCommand;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetByIdDetailEmployeeAddressQuery;
using AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByEmployeeIdForCreateQuery;
using AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetSelectedAddressQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using Messages = AutoSpareParts.Application.Features.EmployeeAddresses.Constants.Messages;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EmployeeAddressController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeAddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Reading, Definition = EndpointDefinition.EmployeeAddressEndpointGetIndex)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Reading, Definition = EndpointDefinition.EmployeeAddressEndpointGetCreateEmployeeAddress)]
    public async Task<IActionResult> CreateEmployeeAddress(int employeeId)
    {
        var dresult = await _mediator.Send(new GetEmployeeAddressByEmployeeIdForCreateQueryRequest()
        {
            EmployeeId = employeeId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400});
    }

    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Writing, Definition = EndpointDefinition.EmployeeAddressEndpointPostCreateEmployeeAddress)]
    public async Task<IActionResult> CreateEmployeeAddress(EmployeeAddressDto employeeAddressDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new CreateEmployeeAddressCommandRequest
            {
                EmployeeAddressDto = employeeAddressDto
            });
            if (dresult.Result.Message == Messages.EmployeeAddressCountMoreThan4)
            {
                var getSelectedEmployeeAddressResult = await _mediator.Send(new GetSelectedEmployeeAddressQueryRequest()
                {
                    EmployeeAddressDto = employeeAddressDto
                });
                ModelState.AddModelError(nameof(Messages.EmployeeAddressCountMoreThan4), Messages.EmployeeAddressCountMoreThan4);
                return View(getSelectedEmployeeAddressResult.Result.Data);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData[nameof(this.CreateEmployeeAddress) + "Success"] = true;
                return RedirectToAction("CreateEmployeeAddress", "EmployeeAddress" ,new { area = "Admin", employeeId = employeeAddressDto.EmployeeId});
            }
        }
        var selectedAddressResult = await _mediator.Send(new GetSelectedEmployeeAddressQueryRequest()
        {
            EmployeeAddressDto = employeeAddressDto
        });
        return View( selectedAddressResult.Result.Data);
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Reading, Definition = EndpointDefinition.EmployeeAddressEndpointGetUpdateEmployeeAddress)]
    public async Task<IActionResult> UpdateEmployeeAddress(int employeeAddressId)
    {
        if (employeeAddressId > 0)
        {
            var dresult = await _mediator.Send(new GetByIdUpdateEmployeeAddressQueryRequest()
            {
                Id = employeeAddressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
            
        }
        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400});
    }

    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Updating, Definition = EndpointDefinition.EmployeeAddressEndpointPostUpdateEmployeeAddress)]
    public async Task<IActionResult> UpdateEmployeeAddress(EmployeeAddressDto employeeAddressDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new UpdateEmployeeAddressCommandRequest
            {
                EmployeeAddressDto = employeeAddressDto, 
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData[nameof(this.UpdateEmployeeAddress) + "Success"] = true;
                return RedirectToAction("Profile", "EmployeeAccount" ,new { area = "Admin", id = employeeAddressDto.EmployeeId});
            }
        }
        var selectedAddressResult = await _mediator.Send(new GetSelectedEmployeeAddressQueryRequest()
        {
            EmployeeAddressDto = employeeAddressDto
        });
        return View(selectedAddressResult.Result.Data);
    }


    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Reading, Definition = EndpointDefinition.EmployeeAddressEndpointGetEmployeeAddressDetail)]
    public async Task<IActionResult>  EmployeeAddressDetail(int employeeAddressId)
    {
        if (employeeAddressId > 0)
        {
            var dresult = await _mediator.Send(new GetEmployeeAddressDetailByIdQueryRequest()
            {
                Id = employeeAddressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
    
    
    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeAddress, EndpointType = EndpointType.Deleting, Definition = EndpointDefinition.EmployeeAddressEndpointPostDeleteEmployeeAddress)]
    public async Task<IActionResult> DeleteEmployeeAddress(int employeeAddressId)
    {
        if (employeeAddressId > 0)
        {
            var dresult = await _mediator.Send(new DeleteEmployeeAddressCommandRequest
            {
                Id = employeeAddressId, 
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                var employeeId = dresult.Result.Data.EmployeeId;
                return Json(new { success = true, employeeId });
            }
            return Json(new { success = false});
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }    
}