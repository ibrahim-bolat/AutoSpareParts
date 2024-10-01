using AutoMapper;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;

public class GetByIdRoleQueryHandler:IRequestHandler<GetByIdRoleQueryRequest,GetByIdRoleQueryResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetByIdRoleQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<GetByIdRoleQueryResponse> Handle(GetByIdRoleQueryRequest request, CancellationToken cancellationToken)
    {
        var role = await  _roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
            RoleDto roleDto = _mapper.Map<RoleDto>(role);
            return new GetByIdRoleQueryResponse{
                Result = new DataResult<RoleDto>(ResultStatus.Success, roleDto)
            };
        }
        return new GetByIdRoleQueryResponse{
            Result = new DataResult<RoleDto>(ResultStatus.Error, Messages.RoleNotFound,null)
        };
    }
}