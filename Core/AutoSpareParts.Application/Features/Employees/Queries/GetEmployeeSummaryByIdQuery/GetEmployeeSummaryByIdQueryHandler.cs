using AutoMapper;
using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryByIdQuery;

public class GetEmployeeSummaryByIdQueryHandler:IRequestHandler<GetEmployeeSummaryByIdQueryRequest,GetEmployeeSummaryByIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;

    public GetEmployeeSummaryByIdQueryHandler(UserManager<Employee> employeeManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
    }

    public async Task<GetEmployeeSummaryByIdQueryResponse> Handle(GetEmployeeSummaryByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var employee = await  _employeeManager.FindByIdAsync(request.Id);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                EmployeeSummaryDto employeeSummaryDto = _mapper.Map<EmployeeSummaryDto>(employee);
                return new GetEmployeeSummaryByIdQueryResponse{
                    Result = new DataResult<EmployeeSummaryDto>(ResultStatus.Success, employeeSummaryDto)
                };
            }
            return new GetEmployeeSummaryByIdQueryResponse{
                Result = new DataResult<EmployeeSummaryDto>(ResultStatus.Error, Messages.EmployeeNotActive,null)
            };
        }
        return new GetEmployeeSummaryByIdQueryResponse{
            Result = new DataResult<EmployeeSummaryDto>(ResultStatus.Error, Messages.EmployeeNotFound,null)
        };
    }
}