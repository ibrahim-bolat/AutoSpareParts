using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.LoginEmployeeCommand;

public class LoginEmployeeCommandHandler : IRequestHandler<LoginEmployeeCommandRequest, LoginEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeeSignInManager;

    public LoginEmployeeCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeeSignInManager)
    {
        _employeeManager = employeeManager;
        _employeeSignInManager = employeeSignInManager;
    }

    public async Task<LoginEmployeeCommandResponse> Handle(LoginEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByEmailAsync(request.LoginEmployeeDto.Email);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                await _employeeSignInManager.SignOutAsync();
                SignInResult result = await _employeeSignInManager.PasswordSignInAsync(employee, request.LoginEmployeeDto.Password, request.LoginEmployeeDto.Persistent, request.LoginEmployeeDto.Lock);
                if (result.Succeeded)
                {
                    await _employeeManager.ResetAccessFailedCountAsync(employee);
                    return new LoginEmployeeCommandResponse
                    {
                        Result = new DataResult<LoginEmployeeDto>(ResultStatus.Success, Messages.EmployeeLoggedIn, request.LoginEmployeeDto)
                    };
                }
                await _employeeManager.AccessFailedAsync(employee);
                int failcount = await _employeeManager.GetAccessFailedCountAsync(employee);
                if (failcount == 3)
                {
                    await _employeeManager.SetLockoutEndDateAsync(employee, new DateTimeOffset(DateTime.Now.AddMinutes(30)));
                    return new LoginEmployeeCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.EmployeeAccountLocked)
                    };
                }
                if (result.IsLockedOut)
                {
                    return new LoginEmployeeCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.EmployeeAccountLocked)
                    };
                }
                return new LoginEmployeeCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.EmployeeIncorrectPassword)
                };
            }
            return new LoginEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new LoginEmployeeCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}