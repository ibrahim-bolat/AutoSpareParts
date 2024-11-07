using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Employees.Commands.SetActiveEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Queries.GetPassiveEmployeeListQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Constants;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DeletedAccountController : Controller
{
    private readonly IMediator _mediator;

    public DeletedAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.DeletedAccount, EndpointType = EndpointType.Reading, Definition = "Get DeletedAccount Index Page")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> DeletedUsers(DatatableRequestDto datatableRequestDto)
    {
        var dresult = await _mediator.Send(new GetPassiveEmployeeListQueryRequest()
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
    public async Task<IActionResult> SetActiveUser(int userId)
    {
        var dresult = await _mediator.Send(new SetActiveEmployeeCommandRequest()
        {
            Id = userId.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.EmployeeActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound),Messages.EmployeeActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
        {
            dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
}