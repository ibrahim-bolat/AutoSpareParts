using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.EditEmployeePasswordCommand;

public class EditEmployeePasswordCommandHandler : IRequestHandler<EditEmployeePasswordCommandRequest, EditEmployeePasswordCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeeSignInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditEmployeePasswordCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeeSignInManager, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _employeeSignInManager = employeeSignInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EditEmployeePasswordCommandResponse> Handle(EditEmployeePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result;
        Employee employee = await _employeeManager.FindByIdAsync(request.EditPasswordAccountDto.Id.ToString());
        string contextUserName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (employee.UserName.Equals(contextUserName))
        {
            if (employee != null)
            {
                if (employee.IsActive)
                {
                    var token = await _employeeManager.GeneratePasswordResetTokenAsync(employee);
                    result = await _employeeManager.ResetPasswordAsync(employee, token, request.EditPasswordAccountDto.NewPassword);
                    if (!result.Succeeded)
                    {
                        return new EditEmployeePasswordCommandResponse
                        {
                            Result = new Result(ResultStatus.Error, Messages.ErrorUpdateEmployeePassword, result.Errors.ToList())
                        };
                    }
                    result = await _employeeManager.UpdateSecurityStampAsync(employee);
                    if (!result.Succeeded)
                    {
                        return new EditEmployeePasswordCommandResponse
                        {
                            Result = new Result(ResultStatus.Error, Messages.NotUpdateEmployeeSecurityStamp, result.Errors.ToList())
                        };
                    }
                    await _employeeSignInManager.RefreshSignInAsync(employee);
                    return new EditEmployeePasswordCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.SuccessUpdateEmployeePassword)
                    };
                }
                return new EditEmployeePasswordCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
                };
            }
        }
        return new EditEmployeePasswordCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}