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
using AutoSpareParts.Application.Features.Employees.Queries.GetRoleListByEmployeeIdQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEditEmployeePasswordByIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Features.Employees.Constants;


namespace AutoSpareParts.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class UserOperationController : Controller
    {

        private readonly IMediator _mediator;

        public UserOperationController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Reading, Definition = "Get Employee Index Page")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult>  GetAllUsers(DatatableRequestDto datatableRequestDto)
        {
 
            var dresult = await _mediator.Send(new GetActiveEmployeeListQueryRequest()
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
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Writing, Definition = "Create User")]
        public async Task<IActionResult> CreateUser(CreateEmployeeDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_UserCreateModalPartial", createUserDto);
            }
            var dresult = await _mediator.Send(new CreateEmployeeCommandRequest()
            {
                CreateEmployeeDto = createUserDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_UserCreateModalPartial", createUserDto);   
            }
            return Json(new { success = false });
        }

        [HttpGet]
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Reading, Definition = "Get By EmloyeeId User Summary")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var dresult = await _mediator.Send(new GetEmployeeSummaryByIdQueryRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, user = dresult.Result.Data });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Deleting, Definition = "Delete User")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var dresult = await _mediator.Send(new SetPassiveEmployeeCommandRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError("EmployeeNotActive",
                    Messages.EmployeeNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false});
        }

        [HttpGet]
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Reading, Definition = "Get By EmloyeeId User Role List")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var dresult = await _mediator.Send(new GetRoleListByEmployeeIdQueryRequest()
            {
                EmployeeId=id,
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, roles = dresult.Result.Data });
            }
            return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
        }

        [HttpPost]
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Updating, Definition = "Change Role to User")]
        public async Task<IActionResult> AssignRolesByUserId(string id, List<int> roleIds)
        {
            if (!string.IsNullOrEmpty(id) && roleIds != null)
            {
                var dresult = await _mediator.Send(new AssignRoleListToEmployeeCommandRequest()
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
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Reading, Definition = "Get Edit Password User By EmloyeeId")]
        public async Task<IActionResult> EditPasswordUser(int id)
        {
            var dresult = await _mediator.Send(new GetEditEmployeePasswordByIdQueryRequest()
            {
                Id = id.ToString()
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("PartialViews/_EditPasswordModalPartial",dresult.Result.Data);
            }
            return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
        }
        
        [HttpPost]
        [Endpoint(Menu = MenuDecription.Employee, EndpointType = EndpointType.Updating, Definition = "Edit Password User")]
        public async Task<IActionResult> EditPasswordUser(EditEmployeePasswordDto editPasswordUserDto)
        {
            if (ModelState.IsValid)
            {
                var dresult = await _mediator.Send(new EditEmployeePasswordCommandRequest()
                {
                    EditEmployeePasswordDto = editPasswordUserDto,
                });
                if (dresult.Result.ResultStatus == ResultStatus.Success)
                {
                    return Json(new { success = true });
                }
                if (dresult.Result.ResultStatus == ResultStatus.Error &&
                    dresult.Result.Message.Equals(Messages.EmployeeNotActive))
                {
                    ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
                    return PartialView("PartialViews/_EditPasswordModalPartial",editPasswordUserDto);
                }
                if (dresult.Result.ResultStatus == ResultStatus.Error &&
                    dresult.Result.Message.Equals(Messages.EmployeeNotFound))
                {
                    ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                    return PartialView("PartialViews/_EditPasswordModalPartial",editPasswordUserDto);
                }
                if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
                {
                    dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return PartialView("PartialViews/_EditPasswordModalPartial",editPasswordUserDto);
                }
            }
            return PartialView("PartialViews/_EditPasswordModalPartial",editPasswordUserDto);
        }
    }