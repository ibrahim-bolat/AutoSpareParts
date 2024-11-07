using System.Security.Claims;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ExternalLoginEmployeeCommand;

public class ExternalLoginEmployeeCommandHandler : IRequestHandler<ExternalLoginEmployeeCommandRequest, ExternalLoginEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeSignInManager;

    public ExternalLoginEmployeeCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeSignInManager)
    {
        _employeeManager = employeeManager;
        _employeSignInManager = employeSignInManager;
    }

    public async Task<ExternalLoginEmployeeCommandResponse> Handle(ExternalLoginEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        ExternalLoginInfo loginInfo = await _employeSignInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            return new ExternalLoginEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.LoginInfoNotFound)
            };
        }
        Employee loginEmployee = await _employeeManager.FindByEmailAsync(loginInfo.Principal.FindFirstValue(ClaimTypes.Email));
        if (loginEmployee != null && loginEmployee.IsDeleted)
        {
            return new ExternalLoginEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        SignInResult loginResult = await _employeSignInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, request.IsPersistent);
        if (loginResult.Succeeded)
        {
            return new ExternalLoginEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.EmployeeLoggedIn)
            };
        }
        Employee employee = new Employee
        {
            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
            FirstName = loginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = loginInfo.Principal.FindFirstValue(ClaimTypes.Surname)
        };
        IdentityResult createResult = await _employeeManager.CreateAsync(employee);
        if (createResult.Succeeded)
        {
            IdentityResult addLoginResult = await _employeeManager.AddLoginAsync(employee, loginInfo);
            if (addLoginResult.Succeeded)
            {
                await _employeSignInManager.SignInAsync(employee, request.IsPersistent);
                return new ExternalLoginEmployeeCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.EmployeeLoggedIn)
                };
            }
            return new ExternalLoginEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotLoggedIn, addLoginResult.Errors.ToList())
            };
        }
        return new ExternalLoginEmployeeCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotAdded, createResult.Errors.ToList())
        };
    }
}