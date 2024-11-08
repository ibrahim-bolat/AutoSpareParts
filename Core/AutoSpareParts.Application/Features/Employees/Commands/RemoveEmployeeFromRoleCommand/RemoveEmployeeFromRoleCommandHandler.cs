using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Commands.RemoveUserFromRoleCommand;

public class RemoveEmployeeFromRoleCommandHandler : IRequestHandler<RemoveEmployeeFromRoleCommandRequest, RemoveEmployeeFromRoleCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveEmployeeFromRoleCommandHandler(UserManager<Employee> employeeManager, RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RemoveEmployeeFromRoleCommandResponse> Handle(RemoveEmployeeFromRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        Employee employee = await _employeeManager.FindByIdAsync(request.EmployeeId);
        AppRole role = await _roleManager.FindByIdAsync(request.RoleId);
        if (employee != null && role != null)
        {
            if (employee.IsActive)
            {
                roleResult = await _employeeManager.RemoveFromRoleAsync(employee, role.Name);
                if (roleResult.Succeeded)
                {
                    return new RemoveEmployeeFromRoleCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.RemovedEmployeeFromRole)
                    };
                }
                return new RemoveEmployeeFromRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.NotRemovedEmployeeFromRole, roleResult.Errors.ToList())
                };
            }
            return new RemoveEmployeeFromRoleCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new RemoveEmployeeFromRoleCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}