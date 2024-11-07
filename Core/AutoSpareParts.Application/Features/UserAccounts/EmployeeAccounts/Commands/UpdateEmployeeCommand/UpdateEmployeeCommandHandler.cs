using AutoMapper;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeeCommand;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateEmployeeCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly SignInManager<Employee> _employeeSignInManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateEmployeeCommandHandler(UserManager<Employee> employeeManager, SignInManager<Employee> employeeSignInManager,
        IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _employeeSignInManager = employeeSignInManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        Employee employee = await _employeeManager.FindByIdAsync(request.EmployeeDto.Id.ToString());
        if (employee != null)
        {
            string tempUserName = employee.UserName;
            if (employee.IsActive)
            {
                employee = _mapper.Map(request.EmployeeDto, employee);
                employee.NormalizedEmail = _employeeManager.NormalizeEmail(request.EmployeeDto.Email);
                employee.NormalizedUserName = _employeeManager.NormalizeName(request.EmployeeDto.UserName);
                result = await _employeeManager.UpdateAsync(employee);
                if (!result.Succeeded)
                {
                    return new UpdateEmployeeCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.EmployeeNotUpdated, result.Errors.ToList())
                    };
                }
                result = await _employeeManager.UpdateSecurityStampAsync(employee);
                if (!result.Succeeded)
                {
                    return new UpdateEmployeeCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.NotUpdateEmployeeSecurityStamp, result.Errors.ToList())
                    };
                }
                string userIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                if (!string.IsNullOrEmpty(userIdentityName) && userIdentityName.Equals(tempUserName))
                {
                    await _employeeSignInManager.RefreshSignInAsync(employee);
                }
                return new UpdateEmployeeCommandResponse
                {
                    Result = new DataResult<EmployeeDto>(ResultStatus.Success, Messages.EmployeeUpdated, request.EmployeeDto)
                };

            }
            return new UpdateEmployeeCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new UpdateEmployeeCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}