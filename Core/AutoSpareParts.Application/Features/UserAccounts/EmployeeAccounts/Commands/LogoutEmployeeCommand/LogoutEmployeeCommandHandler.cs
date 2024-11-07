using AutoSpareParts.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.LogoutEmployeeCommand;

public class LogoutEmployeeCommandHandler : IRequestHandler<LogoutEmployeeCommandRequest>
{
    private readonly SignInManager<Employee> _employeeSignInManager;

    public LogoutEmployeeCommandHandler(SignInManager<Employee> employeeSignInManager)
    {
        _employeeSignInManager = employeeSignInManager;
    }

    public async Task Handle(LogoutEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        await _employeeSignInManager.SignOutAsync();
    }
}