using System.Web;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ConfirmEmployeeEmailCommand;

public class ConfirmEmailUserCommandHandler : IRequestHandler<ConfirmEmployeeEmailCommandRequest, ConfirmEmployeeEmailCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;

    public ConfirmEmailUserCommandHandler(UserManager<Employee> employeeManager)
    {
        _employeeManager = employeeManager;
    }

    public async Task<ConfirmEmployeeEmailCommandResponse> Handle(ConfirmEmployeeEmailCommandRequest request, CancellationToken cancellationToken)
    {
        var employee = await _employeeManager.FindByEmailAsync(request.Email);
        if (employee == null)
        {
            return new ConfirmEmployeeEmailCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
            };
        }
        var result = await _employeeManager.ConfirmEmailAsync(employee, HttpUtility.UrlDecode(request.Token));
        if (result.Succeeded)
        {
            return new ConfirmEmployeeEmailCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.ConfirmEmployeeEmail)
            };
        }
        return new ConfirmEmployeeEmailCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.NotConfirmEmployeeEmail, result.Errors.ToList())
        };
    }
}