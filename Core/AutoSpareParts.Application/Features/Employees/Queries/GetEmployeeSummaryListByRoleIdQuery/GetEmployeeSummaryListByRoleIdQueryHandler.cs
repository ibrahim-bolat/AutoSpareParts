using AutoMapper;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryListByRoleIdQuery;

public class GetEmployeeSummaryListByRoleIdQueryHandler : IRequestHandler<GetEmployeeSummaryListByRoleIdQueryRequest, GetEmployeeSummaryListByRoleIdQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetEmployeeSummaryListByRoleIdQueryHandler(UserManager<Employee> employeeManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _employeeManager = employeeManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<GetEmployeeSummaryListByRoleIdQueryResponse> Handle(GetEmployeeSummaryListByRoleIdQueryRequest request, CancellationToken cancellationToken)
    {
        AppRole role = await _roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
            var employeeData = _employeeManager.GetUsersInRoleAsync(role.Name).Result.AsQueryable();
            int pageSize = request.DatatableRequestDto.Length == -1 ? employeeData.Count() : request.DatatableRequestDto.Length;
            int skip = request.DatatableRequestDto.Start;
            var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order[0].Column].Data;
            var sortColumnDirection = request.DatatableRequestDto.Order[0].Dir.ToString();
            if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
            {
                employeeData = employeeData.Where(m => m.FirstName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                               || m.LastName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                               || m.UserName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                               || m.Email.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                Func<Employee, string> orderingFunction = c => sortColumn == nameof(c.FirstName) ? c.FirstName :
                    sortColumn == nameof(c.LastName) ? c.LastName :
                    sortColumn == nameof(c.UserName) ? c.UserName :
                    sortColumn == nameof(c.Email) ? c.Email : c.Id.ToString();

                if (sortColumnDirection == OrderDirType.Desc.ToString())
                {
                    employeeData = employeeData.OrderByDescending(orderingFunction).AsQueryable();
                }
                else
                {
                    employeeData = employeeData.OrderBy(orderingFunction).AsQueryable();
                }
            }
            int recordsTotal = employeeData.Count();
            var data = employeeData.Skip(skip).Take(pageSize).ToList();
            List<EmployeeSummaryDto> employeeSummeryList = _mapper.Map<List<EmployeeSummaryDto>>(data);
            var response = new DatatableResponseDto<EmployeeSummaryDto>
            {
                Draw = request.DatatableRequestDto.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = employeeSummeryList
            };
            return new GetEmployeeSummaryListByRoleIdQueryResponse
            {
                Result = new DataResult<DatatableResponseDto<EmployeeSummaryDto>>(ResultStatus.Success, response)
            };
        }
        return new GetEmployeeSummaryListByRoleIdQueryResponse
        {
            Result = new DataResult<DatatableResponseDto<EmployeeSummaryDto>>(ResultStatus.Error, null)
        };
    }
}