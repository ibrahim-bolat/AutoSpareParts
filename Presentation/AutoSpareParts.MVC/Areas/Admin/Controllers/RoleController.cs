using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.Commands.RemoveUserFromRoleCommand;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryListByRoleIdQuery;
using AutoSpareParts.Application.Features.Roles.Commands.CreateRoleCommand;
using AutoSpareParts.Application.Features.Roles.Commands.SetActiveRoleCommand;
using AutoSpareParts.Application.Features.Roles.Commands.SetPassiveRoleCommand;
using AutoSpareParts.Application.Features.Roles.Commands.UpdateRoleCommand;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Features.Roles.DTOs;
using AutoSpareParts.Application.Features.Roles.Queries.GetRoleByIdQuery;
using AutoSpareParts.Application.Features.Roles.Queries.GetRoleListQuery;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.MVC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;


[Area("Admin")]
public class RoleController : Controller
{
    private readonly IMediator _mediator;
    private readonly string IndexAction = "Index";

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.Role, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetIndex)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetRoleList(DatatableRequestDto datatableRequestDto)
    {

        var dresult = await _mediator.Send(new GetRoleListQueryRequest()
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

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.Role, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetEmployeesOfRole)]
    public async Task<IActionResult> EmployeesOfRole(int id)
    {
        var dresult = await _mediator.Send(new GetRoleByIdQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = nameof(Admin), statusCode = 400 });
    }

    [HttpPost]
    public async Task<IActionResult> EmployeeSummaryListByRoleId(DatatableRequestDto datatableRequestDto, [FromQuery] string id)
    {
        var dresult = await _mediator.Send(new GetEmployeeSummaryListByRoleIdQueryRequest()
        {
            Id = id,
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
    public async Task<IActionResult> RemoveEmployeeFromRole(int employeeId, int roleId)
    {
        var dresult = await _mediator.Send(new RemoveEmployeeFromRoleCommandRequest()
        {
            EmployeeId = employeeId.ToString(),
            RoleId = roleId.ToString()
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
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

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.Role, EndpointType = EndpointType.Writing, Decription = EndpointDecription.PostCreateRole)]
    public async Task<IActionResult> CreateRole(RoleDto roleDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_CreateRoleModalPartial", roleDto);
        }
        var dresult = await _mediator.Send(new CreateRoleCommandRequest()
        {
            RoleDto = roleDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
        {
            dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return PartialView("PartialViews/_CreateRoleModalPartial", roleDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.Role, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostUpdateRole)]
    public async Task<IActionResult> UpdateRole(RoleDto roleDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_UpdateRoleModalPartial", roleDto);
        }
        var dresult = await _mediator.Send(new UpdateRoleCommandRequest()
        {
            RoleDto = roleDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
        {
            dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return PartialView("PartialViews/_UpdateRoleModalPartial", roleDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleNotFound))
        {
            ModelState.AddModelError(nameof(Messages.RoleNotFound), Messages.RoleNotFound);
            return PartialView("PartialViews/_UpdateRoleModalPartial", roleDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.DefaultRole))
        {
            ModelState.AddModelError(nameof(Messages.DefaultRole), Messages.DefaultRole);
            return PartialView("PartialViews/_UpdateRoleModalPartial", roleDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetActiveRole(int id)
    {
        var dresult = await _mediator.Send(new SetActiveRoleCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleNotFound))
        {
            ModelState.AddModelError(nameof(Messages.RoleNotFound), Messages.RoleNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.DefaultRole))
        {
            ModelState.AddModelError(nameof(Messages.DefaultRole), Messages.DefaultRole);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleActive))
        {
            ModelState.AddModelError(nameof(Messages.RoleActive), Messages.RoleActive);
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

    [HttpPost]
    public async Task<IActionResult> SetPassiveRole(int id)
    {
        var dresult = await _mediator.Send(new SetPassiveRoleCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.RoleNotFound))
        {
            ModelState.AddModelError(nameof(Messages.RoleNotFound), Messages.RoleNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.DefaultRole))
        {
            ModelState.AddModelError(nameof(Messages.DefaultRole), Messages.DefaultRole);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.RoleNotActive))
        {
            ModelState.AddModelError(nameof(Messages.RoleNotActive), Messages.RoleNotActive);
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
    [Endpoint(Menu = MenuDefinition.Role, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetRoleById)]
    public async Task<IActionResult> GetRoleById(string id)
    {
        var dresult = await _mediator.Send(new GetRoleByIdQueryRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, role = dresult.Result.Data });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleNotFound))
        {
            ModelState.AddModelError(nameof(Messages.RoleNotFound), Messages.RoleNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        return Json(new { success = false });
    }
}
