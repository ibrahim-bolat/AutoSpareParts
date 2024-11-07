using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEmployeeIdQuery;

public class GetRoleListByEmployeeIdQueryHandler : IRequestHandler<GetRoleListByEmployeeIdQueryRequest,GetRoleListByEmployeeIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<AppRole> _roleManager;

    public GetRoleListByEmployeeIdQueryHandler(UserManager<Employee> employeeManager, RoleManager<AppRole> roleManager)
    {
        _employeeManager = employeeManager;
        _roleManager = roleManager;
    }

    public async Task<GetRoleListByEmployeeIdQueryResponse> Handle(GetRoleListByEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByIdAsync(request.EmployeeId);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                List<AppRole> allActiveRoles =  await _roleManager.Roles.Where(r=>r.IsActive).ToListAsync();
                List<string> employeeRoles = await _employeeManager.GetRolesAsync(employee) as List<string>;
                List<RoleAssignDto> assignRoles = new List<RoleAssignDto>();
                allActiveRoles.ForEach(role => assignRoles.Add(new RoleAssignDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasAssign = employeeRoles != null && employeeRoles.Contains(role.Name)
                }));
                return new GetRoleListByEmployeeIdQueryResponse{
                    Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Success, assignRoles)
                };
            }
            return new GetRoleListByEmployeeIdQueryResponse{
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EmployeeNotActive,null)
            };
        }
        return new GetRoleListByEmployeeIdQueryResponse{
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EmployeeNotFound,null)
        };
    }
}