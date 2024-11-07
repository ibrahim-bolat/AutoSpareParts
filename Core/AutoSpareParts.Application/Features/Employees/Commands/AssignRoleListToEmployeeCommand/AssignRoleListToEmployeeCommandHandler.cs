using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.Employees.Commands.AssignRoleListToEmployeeCommand;

public class AssignRoleListToEmployeeCommandHandler:IRequestHandler<AssignRoleListToEmployeeCommandRequest,AssignRoleListToEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<Employee> _employeeSignInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AssignRoleListToEmployeeCommandHandler(UserManager<Employee> employeeManager, RoleManager<AppRole> roleManager, SignInManager<Employee> employeeSignInManager, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _roleManager = roleManager;
        _employeeSignInManager = employeeSignInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AssignRoleListToEmployeeCommandResponse> Handle(AssignRoleListToEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result;
        Employee employee = await _employeeManager.FindByIdAsync(request.Id);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                List<AppRole> allRoles = await _roleManager.Roles.Where(r=>r.IsActive).ToListAsync();
                foreach (var role in allRoles)
                {
                    if (request.RoleIds.Contains(role.Id))
                    {
                        if (!await _employeeManager.IsInRoleAsync(employee, role.Name))
                            await _employeeManager.AddToRoleAsync(employee, role.Name);
                    }
                    else
                    {
                        if (await _employeeManager.IsInRoleAsync(employee, role.Name))
                            await _employeeManager.RemoveFromRoleAsync(employee, role.Name);
                    }
                }
                result = await _employeeManager.UpdateSecurityStampAsync(employee);
                if (!result.Succeeded)
                {
                    return new AssignRoleListToEmployeeCommandResponse{
                        Result = new Result(ResultStatus.Error, Messages.NotUpdateEmployeeSecurityStamp, result.Errors.ToList())
                    };
                }
                string employeeIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                if (!string.IsNullOrEmpty(employeeIdentityName) && employee.UserName.Equals(employeeIdentityName))
                {
                    await _employeeSignInManager.RefreshSignInAsync(employee);
                }
                return new AssignRoleListToEmployeeCommandResponse{
                    Result = new Result(ResultStatus.Success, Messages.RoleAdded)
                };
            }
            return new AssignRoleListToEmployeeCommandResponse{
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EmployeeNotActive,null)
            };
        }
        return new AssignRoleListToEmployeeCommandResponse{
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EmployeeNotFound,null)
        };
    }
}