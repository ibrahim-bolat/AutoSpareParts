using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Employees.Commands.SetActiveEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Queries.GetPassiveEmployeeListQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.MVC.Controllers;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DeletedEmployeeController : Controller
{
    private readonly IMediator _mediator;

    public DeletedEmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.DeletedAccount, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetDeletedEmployeeIndex)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> GetDeletedEmployeeList(DatatableRequestDto datatableRequestDto)
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
    public async Task<IActionResult> SetActiveEmployee(int employeeId)
    {
        string Index = "Index";
        var dresult = await _mediator.Send(new SetActiveEmployeeCommandRequest()
        {
            Id = employeeId.ToString()
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
        return RedirectToAction(Index, nameof(ErrorController)[..^10] ,new { area = string.Empty, statusCode = 400});
    }
}