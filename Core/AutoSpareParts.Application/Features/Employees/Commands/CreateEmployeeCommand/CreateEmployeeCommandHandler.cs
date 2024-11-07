using AutoMapper;
using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Commands.CreateEmployeeCommand;

public class CreateEmployeeCommandHandler:IRequestHandler<CreateEmployeeCommandRequest,CreateEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(UserManager<Employee> employeeManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult createResult, confirmResult, roleResult;
        Employee newEmployee = _mapper.Map<Employee>(request.CreateEmployeeDto);
        AppRole role = await _roleManager.FindByNameAsync(RoleType.User.ToString());
        if (role == null)
            await _roleManager.CreateAsync(new AppRole { Name = RoleType.User.ToString() });

        createResult = await _employeeManager.CreateAsync(newEmployee, request.CreateEmployeeDto.Password);
        if (createResult.Succeeded)
        {
            var token = await _employeeManager.GenerateEmailConfirmationTokenAsync(newEmployee);
            confirmResult = await _employeeManager.ConfirmEmailAsync(newEmployee, token);
            if (confirmResult.Succeeded)
            {
                roleResult = await _employeeManager.AddToRoleAsync(newEmployee, RoleType.User.ToString());
                if (roleResult.Succeeded)
                {
                    return new CreateEmployeeCommandResponse
                    {
                        Result = new DataResult<CreateEmployeeDto>(ResultStatus.Success, Messages.EmployeeAdded, request.CreateEmployeeDto)
                    };
                }
                return new CreateEmployeeCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.ErrorConfirmEmployee,roleResult.Errors.ToList())
                };
            }
            return new CreateEmployeeCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotAdded,createResult.Errors.ToList())
            };
        }
        return new CreateEmployeeCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotAdded,createResult.Errors.ToList())
        };
    }
}