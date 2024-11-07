using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Commands.SetPassiveEmployeeCommand;

public class SetPassiveEmployeeCommandHandler : IRequestHandler<SetPassiveEmployeeCommandRequest, SetDeletedUserCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeeSignInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetPassiveEmployeeCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeeSignInManager, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _employeeSignInManager = employeeSignInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetDeletedUserCommandResponse> Handle(SetPassiveEmployeeCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        Employee employee = await _employeeManager.FindByIdAsync(request.Id);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                employee.IsActive = false;
                employee.IsDeleted = true;
                employee.ModifiedTime = DateTime.Now;
                employee.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                result = await _employeeManager.UpdateAsync(employee);
                if (!result.Succeeded)
                {
                    return new SetDeletedUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.EmployeeNotUpdated, result.Errors.ToList())
                    };
                }
                result = await _employeeManager.UpdateSecurityStampAsync(employee);
                if (!result.Succeeded)
                {
                    return new SetDeletedUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.NotUpdateEmployeeSecurityStamp, result.Errors.ToList())
                    };
                }
                return new SetDeletedUserCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.EmployeeUpdated)
                };

            }
            return new SetDeletedUserCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
                };
        }
        return new SetDeletedUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
            };
    }
}