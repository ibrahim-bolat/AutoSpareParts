using AutoSpareParts.Application.Features.Employees.Commands.ConfirmEmployeeEmailCommand;
using AutoSpareParts.Application.Features.Employees.Commands.LoginEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Commands.RegisterEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Commands.UpdateEmployeePasswordCommand;
using AutoSpareParts.Application.Features.Employees.Commands.UpdateEmployeeCommand;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.Features.Employees.Commands.EditEmployeePasswordCommand;
using AutoSpareParts.Application.Features.Employees.Commands.ExternalLoginEmployeeCommand;
using AutoSpareParts.Application.Features.Employees.Queries.ForgetEmployeePasswordQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeProfileDetailByIdQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeByIdQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEditEmployeePasswordByIdQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeExternalLoginAuthenticationPropertiesQuery;
using AutoSpareParts.Application.Features.Employees.Queries.LogoutEmployeeQuery;
using AutoSpareParts.Application.Features.Employees.Queries.VerifyEmployeeTokenQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Features.Employees.Constants;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EmployeeController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterEmployeeDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new RegisterEmployeeCommandRequest()
            {
                RegisterEmployeeDto = registerDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["LoginSuccess"] = true;
                return RedirectToAction("Login", "EmployeeAccount", new { area = "Admin" });
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(registerDto);
            }
        }

        return View(registerDto);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{email}/{token}")]
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var dresult = await _mediator.Send(new ConfirmEmployeeEmailCommandRequest()
        {
            Email = email,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            ViewBag.State = true;
            return View("ConfirmEmail");
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ViewBag.State = false;
            return View("ConfirmEmail");
        }

        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string returnUrl = "Index")
    {
        TempData["returnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginEmployeeDto loginDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new LoginEmployeeCommandRequest()
            {
                LoginEmployeeDto = loginDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                if (TempData["returnUrl"]!.Equals("Index") || TempData["returnUrl"].Equals("/"))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                return LocalRedirect(TempData["returnUrl"].ToString()!);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeAccountLocked))
            {
                ModelState.AddModelError("EmployeeAccountLocked", Messages.EmployeeAccountLocked);
                return View(loginDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeIncorrectPassword))
            {
                ModelState.AddModelError("EmployeeIncorrectPassword", Messages.EmployeeIncorrectPassword);
                return View(loginDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
                return View(loginDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                return View(loginDto);
            }
        }
        return View(loginDto);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ExternalLogin(string providerName, bool isPersistent, string returnUrl)
    {
        var dresult = await _mediator.Send(new GetEmployeeExternalLoginAuthenticationPropertiesQueryRequest()
        {
            ProviderName = providerName,
            RedirectUrl = Url.Action("ExternalLoginResponse", "EmployeeAccount", new { providerName = providerName, isPersistent = isPersistent, returnUrl = returnUrl })
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return new ChallengeResult(providerName.Trim(), dresult.Result.Data);
        }
        return RedirectToAction("Login", "EmployeeAccount", new { area = "Admin" });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ExternalLoginResponse(string providerName, bool isPersistent, string returnUrl = "Index")
    {
        var dresult = await _mediator.Send(new ExternalLoginEmployeeCommandRequest()
        {
            IsPersistent = isPersistent
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
            return View("Login");
        }
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            if (returnUrl.Equals("Index") || returnUrl.Equals("/"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return LocalRedirect(returnUrl);
        }
        TempData[$"{providerName}LoginStatus"] = false;
        return RedirectToAction("Login", "EmployeeAccount", new { area = "Admin" });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new LogoutEmployeeQueryRequest());
        return Json(new { success = true });
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult ForgetPass()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ForgetPass(ForgetEmployeePasswordDto forgetPassDto)
    {
        var dresult = await _mediator.Send(new ForgetEmployeePasswordQueryRequest()
        {
            ForgetEmployeePasswordDto = forgetPassDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            TempData["EmailSendStatus"] = true;
            return View();
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.ErrorSendEmployeeEmail))
        {
            TempData["EmailSendStatus"] = false;
            return View();
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
            return View(forgetPassDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
            return View(forgetPassDto);
        }

        return View(forgetPassDto);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{userId}/{token}")]
    public async Task<IActionResult> UpdatePassword(string userId, string token)
    {
        var dresult = await _mediator.Send(new VerifyEmployeeTokenQueryRequest()
        {
            UserId = userId,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View();
        }

        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }

    [AllowAnonymous]
    [HttpPost("[action]/{userId}/{token}")]
    public async Task<IActionResult> UpdatePassword(UpdateEmployeePasswordDto updatePasswordDto, string userId, string token)
    {
        var dresult = await _mediator.Send(new UpdateEmployeePasswordCommandRequest()
        {
            UpdateEmployeePasswordDto = updatePasswordDto,
            EmployeeId = userId,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            TempData["UpdatePasswordStatus"] = true;
            return RedirectToAction("Login", "EmployeeAccount", new { area = "Admin" });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.ErrorUpdateEmployeePassword))
        {
            TempData["UpdatePasswordStatus"] = false;
            return View();
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
            return View(updatePasswordDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
            return View(updatePasswordDto);
        }

        return View(updatePasswordDto);
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAccount, EndpointType = EndpointType.Reading,
        Definition = "Get By EmloyeeId User for Edit Profile")]
    public async Task<IActionResult> EditProfile(int id)
    {
        var dresult = await _mediator.Send(new GetEmployeeByIdQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }

        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeAccount, EndpointType = EndpointType.Updating,
        Definition = "Edit Profile")]
    public async Task<IActionResult> EditProfile(EmployeeDto userDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new UpdateUserCommandRequest()
            {
                EmployeeDto = userDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["EditProfileSuccess"] = true;
                return RedirectToAction("Index", "Employee", new { area = "Admin" });
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
                return View(userDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                return View(userDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(userDto);
            }
        }

        return View(userDto);
    }

    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAccount, EndpointType = EndpointType.Reading,
        Definition = "Get Edit Password EmployeeAccount By EmloyeeId")]
    public async Task<IActionResult> EditPasswordAccount(int id)
    {
        var dresult = await _mediator.Send(new GetEditEmployeePasswordByIdQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDecription.EmployeeAccount, EndpointType = EndpointType.Updating,
        Definition = "Edit Password EmployeeAccount")]
    public async Task<IActionResult> EditPasswordAccount(EditEmployeePasswordDto editPasswordAccountDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new EditEmployeePasswordCommandRequest()
            {
                EditPasswordAccountDto = editPasswordAccountDto,
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["EditPasswordSuccess"] = true;
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError("EmployeeNotActive", Messages.EmployeeNotActive);
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError("EmployeeNotFound", Messages.EmployeeNotFound);
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(editPasswordAccountDto);
            }
        }

        return View(editPasswordAccountDto);
    }


    [HttpGet]
    [Endpoint(Menu = MenuDecription.EmployeeAccount, EndpointType = EndpointType.Reading,
        Definition = "Get By EmloyeeId User for Profile Details")]
    public async Task<IActionResult> Profile(int id)
    {
        var dresult = await _mediator.Send(new GetEmployeeProfileDetailByIdRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }
}