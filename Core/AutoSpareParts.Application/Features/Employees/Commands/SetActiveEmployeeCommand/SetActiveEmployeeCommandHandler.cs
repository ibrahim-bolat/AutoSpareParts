using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Commands.SetActiveEmployeeCommand;

public class SetActiveEmployeeCommandHandler : IRequestHandler<SetActiveEmployeeCommandRequest, SetActiveEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeeSignInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetActiveEmployeeCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeeSignInManager, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _employeeSignInManager = employeeSignInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetActiveEmployeeCommandResponse> Handle(SetActiveEmployeeCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        Employee employee = await _employeeManager.FindByIdAsync(request.Id);
        if (employee != null)
        {
            if (employee.IsDeleted)
            {
                employee.IsActive = true;
                employee.IsDeleted = false;
                employee.ModifiedTime = DateTime.Now;
                employee.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                result = await _employeeManager.UpdateAsync(employee);
                if (!result.Succeeded)
                {
                    return new SetActiveEmployeeCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.EmployeeNotUpdated, result.Errors.ToList())
                    };
                }
                result = await _employeeManager.UpdateSecurityStampAsync(employee);
                if (!result.Succeeded)
                {
                    return new SetActiveEmployeeCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.NotUpdateEmployeeSecurityStamp, result.Errors.ToList())
                    };
                }
                string userIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                if (!string.IsNullOrEmpty(userIdentityName) && employee.UserName.Equals(userIdentityName))
                {
                    await _employeeSignInManager.RefreshSignInAsync(employee);
                }
                return new SetActiveEmployeeCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.EmployeeUpdated)
                };

            }
            return new SetActiveEmployeeCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.EmployeeActive)
                };
        }
        return new SetActiveEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
            };
    }
}