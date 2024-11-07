using System.Web;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeePasswordCommand;

public class UpdateEmployeePasswordCommandHandler : IRequestHandler<UpdateEmployeePasswordCommandRequest, UpdateEmployeePasswordCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;

    public UpdateEmployeePasswordCommandHandler(UserManager<Employee> employeeManager)
    {
        _employeeManager = employeeManager;
    }

    public async Task<UpdateEmployeePasswordCommandResponse> Handle(UpdateEmployeePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByIdAsync(request.EmployeeId);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                IdentityResult result = await _employeeManager.ResetPasswordAsync(employee, HttpUtility.UrlDecode(request.Token), request.UpdateEmployeePasswordDto.Password);
                if (result.Succeeded)
                {
                    await _employeeManager.UpdateSecurityStampAsync(employee);
                    return new UpdateEmployeePasswordCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.SuccessUpdateEmployeePassword)
                    };
                }
                return new UpdateEmployeePasswordCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.ErrorUpdateEmployeePassword, result.Errors.ToList())
                };
            }
            return new UpdateEmployeePasswordCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new UpdateEmployeePasswordCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}