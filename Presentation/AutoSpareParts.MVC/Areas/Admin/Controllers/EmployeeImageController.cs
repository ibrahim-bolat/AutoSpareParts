using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Features.EmployeeImages.Commands.CreateEmployeeImageCommand;
using AutoSpareParts.Application.Features.EmployeeImages.Commands.DeleteEmployeeImageCommand;
using AutoSpareParts.Application.Features.EmployeeImages.Commands.SetEmployeeProfilImageCommand;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.MVC.Controllers;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EmployeeImageController : Controller
{
    private readonly IMediator _mediator;
    private readonly string IndexAction = "Index";

    public EmployeeImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.EmployeeImage, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetCreateEmployeeImage)]
    public IActionResult CreateEmployeeImage(int employeeId)
    {
        CreateEmployeeImageDto createUserImageDto = new CreateEmployeeImageDto();
        createUserImageDto.EmployeeId = employeeId;
        return View(createUserImageDto);
    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.EmployeeImage, EndpointType = EndpointType.Writing, Decription = EndpointDecription.PostCreateEmployeeImage)]
    public async Task<IActionResult> CreateEmployeeImage(CreateEmployeeImageDto createUserImageDto)
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
                ModelState.AddModelError(nameof(Messages.EmployeeImageCountMoreThan4), Messages.EmployeeImageCountMoreThan4);
                return View(createUserImageDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData[nameof(this.CreateEmployeeImage) + "Success"] = true;
                return RedirectToAction(nameof(this.CreateEmployeeImage), nameof(EmployeeImageController)[..^10], new { area = nameof(Admin), employeeId = createUserImageDto.EmployeeId });
            }
        }
        return View(createUserImageDto);
    }
    [HttpPost]
    public async Task<IActionResult> SetEmployeeProfilImage(int id, int employeeId)
    {
        if (id > 0)
        {
            var dresult = await _mediator.Send(new SetEmployeeProfilImageCommandRequest
            {
                Id = id,
                EmployeeId = employeeId,
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }


    [HttpPost]
    [Endpoint(Menu = MenuDefinition.EmployeeImage, EndpointType = EndpointType.Deleting, Decription = EndpointDecription.PostDeleteEmployeeImage)]
    public async Task<IActionResult> DeleteEmployeeImage(int id)
    {
        if (id > 0)
        {
            var dresult = await _mediator.Send(new DeleteEmployeeImageCommandRequest
            {
                Id = id,
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }
}