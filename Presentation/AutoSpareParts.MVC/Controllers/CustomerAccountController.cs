using AutoSpareParts.Application.Features.UserAccounts.CustomerAccounts.DTOs;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

public class CustomerAccountController : Controller
{
    private readonly IMediator _mediator;
    private readonly string IndexAction = "Index";

    public CustomerAccountController(IMediator mediator)
    {
        _mediator = mediator;
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
    public async Task<IActionResult> Login(LoginCustomerDto loginCustomerDto)
    {
        if (!ModelState.IsValid)
        {
        }
        return null;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(string registerDto)
    {

        return View(registerDto);
    }
}