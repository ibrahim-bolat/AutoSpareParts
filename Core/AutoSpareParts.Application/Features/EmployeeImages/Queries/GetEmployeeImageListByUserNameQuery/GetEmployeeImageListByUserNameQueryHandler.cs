using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByUserNameQuery;

public class GetEmployeeImageListByUserNameQueryHandler : IRequestHandler<GetEmployeeImageListByUserNameQueryRequest, GetEmployeeImageListByUserNameQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IMapper _mapper;

    public GetEmployeeImageListByUserNameQueryHandler(UserManager<Employee> employeeManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _mapper = mapper;
    }

    public async Task<GetEmployeeImageListByUserNameQueryResponse> Handle(GetEmployeeImageListByUserNameQueryRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.Users.Include(u => u.EmployeeImages.Where(a => a.IsActive)).FirstOrDefaultAsync(u => u.UserName == request.UserName);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                List<EmployeeImageDto> employeeImageDtos;
                if (employee.EmployeeImages.Count > 0)
                {
                    employeeImageDtos = _mapper.Map<List<EmployeeImageDto>>(employee.EmployeeImages.Where(a => a.IsActive));

                    return new GetEmployeeImageListByUserNameQueryResponse
                    {
                        Result = new DataResult<List<EmployeeImageDto>>(ResultStatus.Success, employeeImageDtos)
                    };
                };
                employeeImageDtos = new List<EmployeeImageDto>{ new EmployeeImageDto
                    {
                        EmployeeId = employee.Id
                    }
                };
                return new GetEmployeeImageListByUserNameQueryResponse
                {
                    Result = new DataResult<List<EmployeeImageDto>>(ResultStatus.Success, employeeImageDtos)
                };
            }
            return new GetEmployeeImageListByUserNameQueryResponse
            {
                Result = new DataResult<List<EmployeeImageDto>>(ResultStatus.Error, Messages.EmployeeNotActive, null)
            };
        }
        return new GetEmployeeImageListByUserNameQueryResponse
        {
            Result = new DataResult<List<EmployeeImageDto>>(ResultStatus.Error, Messages.EmployeeNotFound, null)
        };
    }
}