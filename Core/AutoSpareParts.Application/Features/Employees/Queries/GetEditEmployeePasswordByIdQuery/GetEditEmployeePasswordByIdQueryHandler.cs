using AutoMapper;
using AutoSpareParts.Application.Features.Employees.Constants;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEditEmployeePasswordByIdQuery;

public class GetEditEmployeePasswordByIdQueryHandler:IRequestHandler<GetEditEmployeePasswordByIdQueryRequest,GetEditEmployeePasswordByIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;

    public GetEditEmployeePasswordByIdQueryHandler(UserManager<Employee> employeeManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
    }

    public async Task<GetEditEmployeePasswordByIdQueryResponse> Handle(GetEditEmployeePasswordByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByIdAsync(request.Id);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                EditEmployeePasswordDto editEmployeePasswordDto = _mapper.Map<EditEmployeePasswordDto>(employee);
                return new GetEditEmployeePasswordByIdQueryResponse
                {
                    Result = new DataResult<EditEmployeePasswordDto>(ResultStatus.Success,editEmployeePasswordDto)
                };
            }
            return new GetEditEmployeePasswordByIdQueryResponse{
                Result = new DataResult<EditEmployeePasswordDto>(ResultStatus.Error, Messages.EmployeeNotActive,null)
            };
        }
        return new GetEditEmployeePasswordByIdQueryResponse{
            Result = new DataResult<EditEmployeePasswordDto>(ResultStatus.Error, Messages.EmployeeNotFound,null)
        };
    }
}