using AutoMapper;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeSummaryCardByIdQuery;

public class GetEmployeeSummaryCardByIdQueryHandler : IRequestHandler<GetByIdForUserSummaryCardQueryRequest, GetEmployeeSummaryCardByIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;

    public GetEmployeeSummaryCardByIdQueryHandler(UserManager<Employee> employeeManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
    }

    public async Task<GetEmployeeSummaryCardByIdQueryResponse> Handle(GetByIdForUserSummaryCardQueryRequest request, CancellationToken cancellationToken)
    {
        var employee = await _employeeManager.Users.Include(u => u.EmployeeAddresses).FirstOrDefaultAsync(ea => ea.Id == request.Id && ea.IsActive);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                EmployeeSummaryCardDto employeeSummaryCardDto = _mapper.Map<EmployeeSummaryCardDto>(employee);
                return new GetEmployeeSummaryCardByIdQueryResponse
                {
                    Result = new DataResult<EmployeeSummaryCardDto>(ResultStatus.Success, employeeSummaryCardDto)
                };
            }
            return new GetEmployeeSummaryCardByIdQueryResponse
            {
                Result = new DataResult<EmployeeSummaryCardDto>(ResultStatus.Error, Messages.EmployeeNotActive, null)
            };
        }
        return new GetEmployeeSummaryCardByIdQueryResponse
        {
            Result = new DataResult<EmployeeSummaryCardDto>(ResultStatus.Error, Messages.EmployeeNotFound, null)
        };
    }
}