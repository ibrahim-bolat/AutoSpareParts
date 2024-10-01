using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.RoleOperations.Commands.CreateRoleCommand;
using AutoSpareParts.Application.Features.RoleOperations.Commands.RemoveUserFromRoleCommand;
using AutoSpareParts.Application.Features.RoleOperations.Commands.SetRoleActiveCommand;
using AutoSpareParts.Application.Features.RoleOperations.Commands.SetRolePassiveCommand;
using AutoSpareParts.Application.Features.RoleOperations.Commands.UpdateRoleCommand;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;
using AutoSpareParts.Application.Features.RoleOperations.Queries.GetRoleListQuery;
using AutoSpareParts.Application.Features.RoleOperations.Queries.GetUsersOfTheRoleQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;


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
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.RoleOperation, EndpointType = EndpointType.Reading, Definition = "Get RoleOperation Index Page")]
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
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.RoleOperation, EndpointType = EndpointType.Reading, Definition = "Get Users Of TheRole Index Page")]
        public  async Task<IActionResult> UsersOfTheRole(int id)
        {
            var dresult = await _mediator.Send(new GetByIdRoleQueryRequest()
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
            var dresult = await _mediator.Send(new GetUsersOfTheRoleQueryRequest()
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
            var dresult = await _mediator.Send(new RemoveUserFromRoleCommandRequest()
            {
                UserId = userId.ToString(),
                RoleId = roleId.ToString()
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
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
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.RoleOperation, EndpointType = EndpointType.Writing, Definition = "Create Role")]
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
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.RoleOperation, EndpointType = EndpointType.Updating, Definition = "Update Role")]
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
            var dresult = await _mediator.Send(new SetRoleActiveCommandRequest()
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
            var dresult = await _mediator.Send(new SetRolePassiveCommandRequest()
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
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.RoleOperation, EndpointType = EndpointType.Reading, Definition = "Get By Id Role Details")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var dresult = await _mediator.Send(new GetByIdRoleQueryRequest()
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
