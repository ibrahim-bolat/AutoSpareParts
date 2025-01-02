using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ConfirmEmployeeEmailCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.EditEmployeeAccountPasswordCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ExternalLoginEmployeeCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ForgetEmployeePasswordCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.LoginEmployeeCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.LogoutEmployeeCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.RegisterEmployeeCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeeCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeeAccountPasswordCommand;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEditEmployeeAccountPasswordByIdQuery;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeByIdQuery;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeExternalLoginAuthenticationPropertiesQuery;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeProfileDetailByIdQuery;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.VerifyEmployeeTokenQuery;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.MVC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EmployeeAccountController : Controller
{
    private readonly IMediator _mediator;
    private readonly string IndexAction = "Index";

    public EmployeeAccountController(IMediator mediator)
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
                TempData[nameof(this.Login) + "Success"] = true;
                return RedirectToAction(nameof(this.Login), nameof(EmployeeAccountController)[..^10], new { area = nameof(Admin) });
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
            return View(nameof(this.ConfirmEmail));
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ViewBag.State = false;
            return View(nameof(this.ConfirmEmail));
        }

        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string returnUrl = "Index")
    {
        TempData[nameof(returnUrl)] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginEmployeeDto loginEmployeeDto)
    {
        if (ModelState.IsValid)
        {
            string returnUrl = "returnUrl";
            var dresult = await _mediator.Send(new LoginEmployeeCommandRequest()
            {
                LoginEmployeeDto = loginEmployeeDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                if (string.IsNullOrEmpty(TempData[returnUrl] != null ? TempData[returnUrl].ToString() : string.Empty))
                    return RedirectToAction(IndexAction, nameof(HomeController)[..^10], new { area = nameof(Admin) });
                if (TempData[returnUrl]!.Equals(IndexAction) || TempData[returnUrl].Equals("/"))
                    return RedirectToAction(IndexAction, nameof(HomeController)[..^10], new { area = nameof(Admin) });
                return LocalRedirect(TempData[returnUrl].ToString()!);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeAccountLocked))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeAccountLocked), Messages.EmployeeAccountLocked);
                return View(loginEmployeeDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeIncorrectPassword))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeIncorrectPassword), Messages.EmployeeIncorrectPassword);
                return View(loginEmployeeDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
                return View(loginEmployeeDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
                return View(loginEmployeeDto);
            }
        }
        return View(loginEmployeeDto);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ExternalLogin(string providerName, bool isPersistent, string returnUrl)
    {
        var dresult = await _mediator.Send(new GetEmployeeExternalLoginAuthenticationPropertiesQueryRequest()
        {
            ProviderName = providerName,
            RedirectUrl = Url.Action(nameof(this.ExternalLoginResponse), nameof(EmployeeAccountController)[..^10], new { providerName = providerName, isPersistent = isPersistent, returnUrl = returnUrl })
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return new ChallengeResult(providerName.Trim(), dresult.Result.Data);
        }
        return RedirectToAction(nameof(this.Login), nameof(EmployeeAccountController)[..^10], new { area = nameof(Admin) });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ExternalLoginResponse(string providerName, bool isPersistent, string returnUrl = "Index")
    {
        var dresult = await _mediator.Send(new ExternalLoginEmployeeCommandRequest()
        {
            IsPersistent = isPersistent
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
            return View(nameof(this.Login));
        }
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            if (returnUrl.Equals(IndexAction) || returnUrl.Equals("/"))
            {
                return RedirectToAction(IndexAction, nameof(HomeController)[..^10], new { area = nameof(Admin) });
            }
            return LocalRedirect(returnUrl);
        }
        TempData[$"{providerName}LoginStatus"] = false;
        return RedirectToAction(nameof(this.Login), nameof(EmployeeAccountController)[..^10], new { area = nameof(Admin) });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new LogoutEmployeeCommandRequest());
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
        string emailSendStatus = "EmailSendStatus";
        var dresult = await _mediator.Send(new ForgetEmployeePasswordCommandRequest()
        {
            ForgetEmployeePasswordDto = forgetPassDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            TempData[emailSendStatus] = true;
            return View();
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.ErrorSendEmployeeEmail))
        {
            TempData[emailSendStatus] = false;
            return View();
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
            return View(forgetPassDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
            return View(forgetPassDto);
        }

        return View(forgetPassDto);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{employeeId}/{token}")]
    public async Task<IActionResult> UpdatePassword(string employeeId, string token)
    {
        var dresult = await _mediator.Send(new VerifyEmployeeTokenQueryRequest()
        {
            EmployeeId = employeeId,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View();
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = string.Empty, statusCode = 400 });
    }

    [AllowAnonymous]
    [HttpPost("[action]/{employeeId}/{token}")]
    public async Task<IActionResult> UpdatePassword(UpdateEmployeePasswordDto updatePasswordDto, string employeeId, string token)
    {
        string updatePasswordStatus = "UpdatePasswordStatus";
        var dresult = await _mediator.Send(new UpdateEmployeeAccountPasswordCommandRequest()
        {
            UpdateEmployeePasswordDto = updatePasswordDto,
            EmployeeId = employeeId,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            TempData[updatePasswordStatus] = true;
            return RedirectToAction(nameof(this.Login), nameof(EmployeeAccountController)[..^10], new { area = nameof(Admin) });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.ErrorUpdateEmployeePassword))
        {
            TempData[updatePasswordStatus] = false;
            return View();
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
            return View(updatePasswordDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
        {
            ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
            return View(updatePasswordDto);
        }

        return View(updatePasswordDto);
    }

    [HttpGet]
    [Endpoint(Menu = MenuDefinition.EmployeeAccount, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetEditEmployeeProfile)]
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

        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = string.Empty, statusCode = 400 });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.EmployeeAccount, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostEditEmployeeProfile)]
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
                TempData[nameof(this.EditProfile) + "Success"] = true;
                return RedirectToAction(IndexAction, nameof(EmployeeController)[..^10], new { area = nameof(Admin) });
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
                return View(userDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
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
    [Endpoint(Menu = MenuDefinition.EmployeeAccount, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetEditEmployeePassword)]
    public async Task<IActionResult> EditPassword(int id)
    {
        var dresult = await _mediator.Send(new GetEditEmployeeAccountPasswordByIdQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = string.Empty, statusCode = 400 });
    }

    [HttpPost]
    [Endpoint(Menu = MenuDefinition.EmployeeAccount, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostEditEmployeePassword)]
    public async Task<IActionResult> EditPassword(EditEmployeeAccountPasswordDto editPasswordAccountDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new EditEmployeeAccountPasswordCommandRequest()
            {
                EditPasswordAccountDto = editPasswordAccountDto,
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData[nameof(this.EditPassword) + "Success"] = true;
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotActive))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotActive), Messages.EmployeeNotActive);
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.EmployeeNotFound))
            {
                ModelState.AddModelError(nameof(Messages.EmployeeNotFound), Messages.EmployeeNotFound);
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
    [Endpoint(Menu = MenuDefinition.EmployeeAccount, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetEmployeeProfileDetail)]
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
        return RedirectToAction(IndexAction, nameof(ErrorController)[..^10], new { area = string.Empty, statusCode = 400 });
    }
}