using AutoSpareParts.Application.Features.Endpoints.Constants;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Endpoint = AutoSpareParts.Domain.Entities.Endpoint;

namespace AutoSpareParts.Application.Features.Endpoints.Commands.AssignRoleListToEndpointsCommand;

public class AssignRoleListToEndpointsCommandHandler : IRequestHandler<
    AssignRoleListToEndpointsCommandRequest, AssignRoleListToEndpointsCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<AppRole> _roleManager;

    public AssignRoleListToEndpointsCommandHandler(IUnitOfWork unitOfWork , RoleManager<AppRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    public async Task<AssignRoleListToEndpointsCommandResponse> Handle(
        AssignRoleListToEndpointsCommandRequest request, CancellationToken cancellationToken)
    {
        
        Endpoint endpoint = await _unitOfWork.Endpoints.GetAsync(predicate:a => a.Id == request.Id && a.IsActive,include:e => e.Include(endpoint=>endpoint.AppRoles));
        if (endpoint != null)
        {
            List<AppRole> allRoles = await _roleManager.Roles.Where(r=>r.IsActive).ToListAsync();
            foreach (var role in allRoles)
            {
                if (request.RoleIds.Contains(role.Id))
                {
                    if (!endpoint.AppRoles.Any(r => r.Id ==role.Id))
                    {
                        AppRole appRole = allRoles.Where(a=>a.Id==role.Id).FirstOrDefault();
                        endpoint.AppRoles.Add(appRole);  
                    }
                }
                else
                {
                    if (endpoint.AppRoles.Any(r => r.Id == role.Id))
                    {
                        AppRole appRole = allRoles.Where(a=>a.Id==role.Id).FirstOrDefault();
                        endpoint.AppRoles.Remove(appRole);  
                    }
                }
            }
            await _unitOfWork.SaveAsync();
            return new AssignRoleListToEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.RoleAdded)
            };
        }

        return new AssignRoleListToEndpointsCommandResponse
        {
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.EndpointNotFound, null)
        };
    }
}