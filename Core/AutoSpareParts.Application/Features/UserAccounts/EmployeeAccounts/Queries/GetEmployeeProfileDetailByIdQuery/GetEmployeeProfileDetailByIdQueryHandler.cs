using AutoMapper;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeProfileDetailByIdQuery;

public class GetEmployeeProfileDetailByIdQueryHandler : IRequestHandler<GetEmployeeProfileDetailByIdRequest, GetEmployeeProfileDetailByIdResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;

    public GetEmployeeProfileDetailByIdQueryHandler(UserManager<Employee> employeeManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
    }

    public async Task<GetEmployeeProfileDetailByIdResponse> Handle(GetEmployeeProfileDetailByIdRequest request, CancellationToken cancellationToken)
    {
        var employee = await _employeeManager.Users.Include(u => u.EmployeeAddresses).FirstOrDefaultAsync(ea => ea.Id == request.Id && ea.IsActive);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
                List<EmployeeAddressSummaryDto> employeeAddressSummaryDto = _mapper.Map<List<EmployeeAddressSummaryDto>>(employee.EmployeeAddresses.Where(a => a.IsActive));
                EmployeeDetailDto employeeDetailDto = new EmployeeDetailDto()
                {
                    EmployeeDto = employeeDto,
                    EmployeeAddressSummaryDtos = employeeAddressSummaryDto
                };
                return new GetEmployeeProfileDetailByIdResponse
                {
                    Result = new DataResult<EmployeeDetailDto>(ResultStatus.Success, employeeDetailDto)
                };
            }
            return new GetEmployeeProfileDetailByIdResponse
            {
                Result = new DataResult<EmployeeDetailDto>(ResultStatus.Error, Messages.EmployeeNotActive, null)
            };
        }
        return new GetEmployeeProfileDetailByIdResponse
        {
            Result = new DataResult<EmployeeDetailDto>(ResultStatus.Error, Messages.EmployeeNotFound, null)
        };
    }
}