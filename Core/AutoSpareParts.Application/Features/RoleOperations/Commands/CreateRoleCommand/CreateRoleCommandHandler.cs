using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Helpers.SeoHelper;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.RoleOperations.Commands.CreateRoleCommand;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;

    public CreateRoleCommandHandler(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult = null;
        string fixedRoleName = SeoHelper.ToSeoUrl(request.RoleDto.Name);
        string roleName = char.ToUpperInvariant(fixedRoleName[0]) + fixedRoleName.Substring(1);
        roleResult = await _roleManager.CreateAsync(new AppRole { Name = roleName });
        if (roleResult.Succeeded)
        {
            return new CreateRoleCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.RoleAdded)
            };
        }
        return new CreateRoleCommandResponse
        {
                Result = new Result(ResultStatus.Error, Messages.RoleNotAdded,roleResult.Errors.ToList())
        };
    }
}