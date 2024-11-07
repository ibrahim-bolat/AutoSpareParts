using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Commands.EditEmployeePasswordCommand;

public class UpdatePasswordUserCommandHandler:IRequestHandler<EditEmployeePasswordCommandRequest,EditEmployeePasswordCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeeSignInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePasswordUserCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeeSignInManager, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _employeeSignInManager = employeeSignInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EditEmployeePasswordCommandResponse> Handle(EditEmployeePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result;
        Employee employee = await _employeeManager.FindByIdAsync(request.EditEmployeePasswordDto.Id.ToString());
        if(employee!=null)
        {
            if (employee.IsActive)
            {
                var token = await _employeeManager.GeneratePasswordResetTokenAsync(employee);
                result = await _employeeManager.ResetPasswordAsync(employee, token, request.EditEmployeePasswordDto.NewPassword);
                if (!result.Succeeded)
                {
                    return new EditEmployeePasswordCommandResponse{
                        Result = new Result(ResultStatus.Error, Messages.ErrorUpdateEmployeePassword,result.Errors.ToList())
                    };
                }
                result = await _employeeManager.UpdateSecurityStampAsync(employee);
                if (!result.Succeeded)
                {
                    return new EditEmployeePasswordCommandResponse{
                        Result = new Result(ResultStatus.Error, Messages.NotUpdateEmployeeSecurityStamp,result.Errors.ToList())
                    };
                }
                string userIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                if (!string.IsNullOrEmpty(userIdentityName) && employee.UserName.Equals(userIdentityName))
                {
                    await _employeeSignInManager.RefreshSignInAsync(employee);
                }
                return new EditEmployeePasswordCommandResponse{
                    Result = new Result(ResultStatus.Success, Messages.SuccessUpdateEmployeePassword)
                };
            }
            return new EditEmployeePasswordCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new EditEmployeePasswordCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}