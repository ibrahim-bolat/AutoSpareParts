using AutoSpareParts.Application.Features.Employees.Commands.AssignRoleListToEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Commands.CreateEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Commands.SetPassiveEmployeeCommand;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.Commands.EditEmployeePasswordCommand;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Features.Employees.Queries.GetActiveEmployeeListQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryByIdQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEditEmployeePasswordByIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEmployeeIdQuery;
using AutoSpareParts.MVC.Controllers;


namespace AutoSpareParts.MVC.Areas.Admin.Controllers;


[Area("Admin")]
public class EmployeeController : Controller
{

    private readonly IMediator _mediator;
    private readonly string IndexAction = "Index";

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetIndex)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> GetEmployeeList(DatatableRequestDto datatableRequestDto)
    {

        var dresult = await _mediator.Send(new GetActiveEmployeeListQueryRequest()
        {
            DatatableRequestDto = datatableRequestDto
        });
        var jsonData = new
        {
            draw = dresult.Result.Data.Draw,
            recordsFiltered = dresult.Result.Data.RecordsFiltered,
            recordsTotal = dresult.Result.Data.RecordsTotal,
            data = dresult.Result.Data.Data,
            isSusccess = true
        };
        return Ok(jsonData);

    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Writing, Decription = EndpointDecription.PostCreateEmployee)]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_CreateEmployeeModalPartial", createEmployeeDto);
        }
        var dresult = await _mediator.Send(new CreateEmployeeCommandRequest()
        {
            CreateEmployeeDto = createEmployeeDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
        {
            dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return PartialView("PartialViews/_CreateEmployeeModalPartial", createEmployeeDto);
        }
        return Json(new { success = false });
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetEmployeeSummaryById)]
    public async Task<IActionResult> GetEmployeeSummaryById(string id)
    {
        var dresult = await _mediator.Send(new GetEmployeeSummaryByIdQueryRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, employee = dresult.Result.Data });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        return Json(new { success = false });
    }


    [HttpPost]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Deleting, Decription = EndpointDecription.PostDeleteEmployee)]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        var dresult = await _mediator.Send(new SetPassiveEmployeeCommandRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
        {
            dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        return Json(new { success = false });
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetRoleListByEmployeeId)]
    public async Task<IActionResult> GetRoleListByEmployeeId(string id)
    {
        var dresult = await _mediator.Send(new GetRoleListByEmployeeIdQueryRequest()
        {
            EmployeeId = id,
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, roles = dresult.Result.Data });
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostAssignRoleListToEmployee)]
    public async Task<IActionResult> AssignRoleListToEmployee(string id, List<int> roleIds)
    {
        if (!string.IsNullOrEmpty(id) && roleIds != null)
        {
            var dresult = await _mediator.Send(new AssignRoleListToEmployeeCommandRequest()
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
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetEditEmployeePasswordById)]
    public async Task<IActionResult> GetEditEmployeePasswordById(int id)
    {
        var dresult = await _mediator.Send(new GetEditEmployeePasswordByIdQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return PartialView("PartialViews/_EditEmployeePasswordModalPartial", dresult.Result.Data);
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.Employee, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostEditEmployeePassword)]
    public async Task<IActionResult> EditEmployeePassword(EditEmployeePasswordDto editEmployeePasswordDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new EditEmployeePasswordCommandRequest()
            {
                EditEmployeePasswordDto = editEmployeePasswordDto,
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
                return PartialView("PartialViews/_EditEmployeePasswordModalPartial", editEmployeePasswordDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
                return PartialView("PartialViews/_EditEmployeePasswordModalPartial", editEmployeePasswordDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_EditEmployeePasswordModalPartial", editEmployeePasswordDto);
            }
        }
        return PartialView("PartialViews/_EditEmployeePasswordModalPartial", editEmployeePasswordDto);
    }
}