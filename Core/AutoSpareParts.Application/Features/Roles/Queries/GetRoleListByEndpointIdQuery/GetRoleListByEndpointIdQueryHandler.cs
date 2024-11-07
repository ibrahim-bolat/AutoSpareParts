using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleListByEndpointIdQuery;

public class GetRoleListByEndpointIdQueryHandler : IRequestHandler<GetRoleListByEndpointIdQueryRequest, GetRoleListByEndpointIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<AppRole> _roleManager;

    public GetRoleListByEndpointIdQueryHandler(IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    public async Task<GetRoleListByEndpointIdQueryResponse> Handle(GetRoleListByEndpointIdQueryRequest request, CancellationToken cancellationToken)
    {
        Endpoint endpoint = await _unitOfWork.Endpoints.GetAsync(predicate: a => a.Id.ToString() == request.Id, include: e => e.Include(endpoint => endpoint.AppRoles));
        if (endpoint != null)
        {
            if (endpoint.IsActive)
            {
                List<AppRole> allActiveRoles = await _roleManager.Roles.Where(r => r.IsActive).ToListAsync();
                List<AppRole> endpointRoles = endpoint.AppRoles;
                List<RoleAssignDto> assignRoles = new List<RoleAssignDto>();
                allActiveRoles.ForEach(role => assignRoles.Add(new RoleAssignDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasAssign = endpointRoles != null && endpointRoles.Any(r => r.Id == role.Id)
                }));
                return new GetRoleListByEndpointIdQueryResponse
                {
                    Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Success, assignRoles)
                };
            }
            return new GetRoleListByEndpointIdQueryResponse
            {
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EmployeeNotActive, null)
            };
        }
        return new GetRoleListByEndpointIdQueryResponse
        {
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EmployeeNotFound, null)
        };
    }
}