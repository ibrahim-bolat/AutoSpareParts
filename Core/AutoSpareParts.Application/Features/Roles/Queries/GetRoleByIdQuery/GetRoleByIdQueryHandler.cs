using AutoMapper;
using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Features.Roles.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Roles.Queries.GetRoleByIdQuery;

public class GetRoleByIdQueryHandler:IRequestHandler<GetRoleByIdQueryRequest,GetRoleByIdQueryResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetRoleByIdQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var role = await  _roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
            RoleDto roleDto = _mapper.Map<RoleDto>(role);
            return new GetRoleByIdQueryResponse{
                Result = new DataResult<RoleDto>(ResultStatus.Success, roleDto)
            };
        }
        return new GetRoleByIdQueryResponse{
            Result = new DataResult<RoleDto>(ResultStatus.Error, Messages.RoleNotFound,null)
        };
    }
}