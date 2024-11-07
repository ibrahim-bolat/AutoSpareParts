using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.Commands.CreateRoleCommand;
using AutoSpareParts.Application.Features.Employees.Commands.RemoveUserFromRoleCommand;
using AutoSpareParts.Application.Features.Employees.Commands.SetActiveRoleCommand;
using AutoSpareParts.Application.Features.Employees.Commands.SetPassiveRoleCommand;
using AutoSpareParts.Application.Features.Employees.Commands.UpdateRoleCommand;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Features.Employees.Queries.GetRoleByIdQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetRoleListQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeesOfRoleQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Features.Employees.Constants;


namespace AutoSpareParts.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class RoleOperationController : Controller
    {
        private readonly IMediator _mediator;

        public RoleOperationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Endpoint(Menu = MenuDecription.Role, EndpointType = EndpointType.Reading, Definition = "Get Role Index Page")]
        public IActionResult  Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAllRoles(DatatableRequestDto datatableRequestDto)
        {
            
            var dresult = await _mediator.Send(new GetRoleListQueryRequest()
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

        [HttpGet]
        [Endpoint(Menu = MenuDecription.Role, EndpointType = EndpointType.Reading, Definition = "Get Users Of TheRole Index Page")]
        public  async Task<IActionResult> UsersOfTheRole(int id)
        {
            var dresult = await _mediator.Send(new GetRoleByIdQueryRequest()
            {
                Id=id.ToString()
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
            return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
        }
        
        [HttpPost]
        public  async Task<IActionResult> UsersOfTheRole(DatatableRequestDto datatableRequestDto,[FromQuery]string id)
        {
            var dresult = await _mediator.Send(new GetEmployeesOfRoleQueryRequest()
            {
                Id=id,
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
        public  async Task<IActionResult> RemoveUserFromRole(int userId,int roleId)
        {
            var dresult = await _mediator.Send(new RemoveEmployeeFromRoleCommandRequest()
            {
                UserId = userId.ToString(),
                RoleId = roleId.ToString()
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        [Endpoint(Menu = MenuDecription.Role, EndpointType = EndpointType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_RoleCreateModalPartial", roleDto);
            }
            var dresult = await _mediator.Send(new CreateRoleCommandRequest()
            {
                RoleDto = roleDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_RoleCreateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        [Endpoint(Menu = MenuDecription.Role, EndpointType = EndpointType.Updating, Definition = "Update Role")]
        public async Task<IActionResult> UpdateRole(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);
            }
            var dresult = await _mediator.Send(new UpdateRoleCommandRequest()
            {
                RoleDto = roleDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleNotFound))
            {
                ModelState.AddModelError("RoleActive", Messages.RoleNotFound);
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleDefaultRole))
            {
                ModelState.AddModelError("RoleDefaultRole", Messages.RoleDefaultRole);
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        
        [HttpPost]
        public async Task<IActionResult> SetRoleActive(int id)
        {
            var dresult = await _mediator.Send(new SetActiveRoleCommandRequest()
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
                ModelState.AddModelError("RoleNotFound", Messages.RoleNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleDefaultRole))
            {
                ModelState.AddModelError("RoleDefaultRole", Messages.RoleDefaultRole);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleActive))
            {
                ModelState.AddModelError("RoleActive", Messages.RoleActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }
        
        [HttpPost]
        public async Task<IActionResult> SetRolePassive(int id)
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
                ModelState.AddModelError("RoleNotFound", Messages.RoleNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleDefaultRole))
            {
                ModelState.AddModelError("RoleDefaultRole", Messages.RoleDefaultRole);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleNotActive))
            {
                ModelState.AddModelError("RoleNotActive", Messages.RoleNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }
        
        [HttpGet]
        [Endpoint(Menu = MenuDecription.Role, EndpointType = EndpointType.Reading, Definition = "Get By EmloyeeId Role Details")]
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
                ModelState.AddModelError("RoleNotFound", Messages.RoleNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }
    }
