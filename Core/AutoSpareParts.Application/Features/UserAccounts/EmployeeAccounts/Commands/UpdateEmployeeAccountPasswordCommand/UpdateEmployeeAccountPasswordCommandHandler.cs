using System.Web;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeeAccountPasswordCommand;

public class UpdateEmployeePasswordCommandHandler : IRequestHandler<UpdateEmployeeAccountPasswordCommandRequest, UpdateEmployeeAccountPasswordCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;

    public UpdateEmployeePasswordCommandHandler(UserManager<Employee> employeeManager)
    {
        _employeeManager = employeeManager;
    }

    public async Task<UpdateEmployeeAccountPasswordCommandResponse> Handle(UpdateEmployeeAccountPasswordCommandRequest request, CancellationToken cancellationToken)
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
                    return new UpdateEmployeeAccountPasswordCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.SuccessUpdateEmployeePassword)
                    };
                }
                return new UpdateEmployeeAccountPasswordCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.ErrorUpdateEmployeePassword, result.Errors.ToList())
                };
            }
            return new UpdateEmployeeAccountPasswordCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new UpdateEmployeeAccountPasswordCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}