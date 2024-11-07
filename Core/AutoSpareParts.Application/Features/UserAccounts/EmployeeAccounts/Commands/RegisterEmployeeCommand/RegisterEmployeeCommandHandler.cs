using AutoMapper;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.RegisterEmployeeCommand;

public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommandRequest, RegisterEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public RegisterEmployeeCommandHandler(UserManager<Employee> employeeManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RegisterEmployeeCommandResponse> Handle(RegisterEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        Employee employee = _mapper.Map<Employee>(request.RegisterEmployeeDto);
        AppRole role = await _roleManager.FindByNameAsync(RoleType.Employee.ToString());
        if (role == null) await _roleManager.CreateAsync(new AppRole { Name = RoleType.Employee.ToString() });
        IdentityResult userResult = await _employeeManager.CreateAsync(employee, request.RegisterEmployeeDto.Password);
        IdentityResult roleResult;
        if (userResult.Succeeded)
        {
            roleResult = await _employeeManager.AddToRoleAsync(employee, RoleType.Employee.ToString());
            if (roleResult.Succeeded)
            {
                return new RegisterEmployeeCommandResponse
                {
                    Result = new DataResult<RegisterEmployeeDto>(ResultStatus.Success, Messages.EmployeeAdded, request.RegisterEmployeeDto)
                };
            }
            return new RegisterEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotAdded, roleResult.Errors.ToList())
            };
        }
        return new RegisterEmployeeCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotAdded, userResult.Errors.ToList())
        };
    }
}