using AutoMapper;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeByIdQuery;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQueryRequest, GetEmployeeByIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(UserManager<Employee> employeeManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
    }

    public async Task<GetEmployeeByIdQueryResponse> Handle(GetEmployeeByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByIdAsync(request.Id);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
                return new GetEmployeeByIdQueryResponse
                {
                    Result = new DataResult<EmployeeDto>(ResultStatus.Success, employeeDto)
                };
            }
            return new GetEmployeeByIdQueryResponse
            {
                Result = new DataResult<EmployeeDto>(ResultStatus.Error, Messages.EmployeeNotActive, null)
            };
        }
        return new GetEmployeeByIdQueryResponse
        {
            Result = new DataResult<EmployeeDto>(ResultStatus.Error, Messages.EmployeeNotFound, null)
        };
    }
}