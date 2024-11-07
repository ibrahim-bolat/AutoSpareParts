using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.Features.EmployeeImages.Commands.CreateEmployeeImageCommand;
using AutoSpareParts.Application.Features.EmployeeImages.Commands.DeleteEmployeeImageCommand;
using AutoSpareParts.Application.Features.EmployeeImages.Commands.SetEmployeeProfilImageCommand;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Messages = AutoSpareParts.Application.Features.EmployeeImages.Constants.Messages;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class UserImageController : Controller
{
    private readonly IMediator _mediator;

    public UserImageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeImage, EndpointType = EndpointType.Reading, Definition = "Get By EmloyeeId User for Create EmployeeImage")]
    public IActionResult CreateUserImage(int userId)
    {
        CreateEmployeeImageDto createUserImageDto = new CreateEmployeeImageDto();
        createUserImageDto.EmployeeId = userId;
        return View(createUserImageDto);
    }
    
    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeImage, EndpointType = EndpointType.Writing, Definition = "Create EmployeeImage")]
    public async Task<IActionResult> CreateUserImage(CreateEmployeeImageDto createUserImageDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new CreateEmployeeImageCommandRequest
            {
                CreateEmployeeImageDto = createUserImageDto, 
                CreatedByName = User.Identity?.Name
            });
            if (dresult.Result.Message == Messages.EmployeeImageCountMoreThan4)
            { 
                ModelState.AddModelError("EmployeeImageCountMoreThan4", Messages.EmployeeImageCountMoreThan4);
                return View(createUserImageDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["CreateUserImageSuccess"] = true;
                return RedirectToAction("createUserImage", "EmployeeImage" ,new { area = "Admin" ,userId=createUserImageDto.EmployeeId});
            }
        }
        return View(createUserImageDto);
    }
    [HttpPost]
    public async Task<IActionResult> SetProfilImage(int id,int userId)
    {
        if (id>0)
        {
            var dresult = await _mediator.Send(new SetEmployeeProfilImageCommandRequest
            {
                Id = id,
                EmployeeId = userId,
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
    
        
    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeImage, EndpointType = EndpointType.Deleting, Definition = "Delete EmployeeImage")]
    public async Task<IActionResult> DeleteUserImage(int id)
    {
        if (id > 0)
        {
            var dresult = await _mediator.Send(new DeleteEmployeeImageCommandRequest
            {
                Id = id, 
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
}