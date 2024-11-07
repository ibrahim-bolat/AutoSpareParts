using AutoMapper;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEditEmployeePasswordByIdQuery;

public class GetEditPasswordAccountByIdQueryHandler : IRequestHandler<GetEditEmployeePasswordByIdQueryRequest, GetEditEmployeePasswordByIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEditPasswordAccountByIdQueryHandler(UserManager<Employee> employeeManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetEditEmployeePasswordByIdQueryResponse> Handle(GetEditEmployeePasswordByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByIdAsync(request.Id);
        string contextUserName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (employee.UserName.Equals(contextUserName))
        {
            if (employee != null)
            {
                if (employee.IsActive)
                {
                    EditEmployeePasswordDto editEmployeePasswordDto = _mapper.Map<EditEmployeePasswordDto>(employee);
                    return new GetEditEmployeePasswordByIdQueryResponse
                    {
                        Result = new DataResult<EditEmployeePasswordDto>(ResultStatus.Success, editEmployeePasswordDto)
                    };
                }
                return new GetEditEmployeePasswordByIdQueryResponse
                {
                    Result = new DataResult<EditEmployeePasswordDto>(ResultStatus.Error, Messages.EmployeeNotActive, null)
                };
            }
        }
        return new GetEditEmployeePasswordByIdQueryResponse
        {
            Result = new DataResult<EditEmployeePasswordDto>(ResultStatus.Error, Messages.EmployeeNotFound, null)
        };
    }
}