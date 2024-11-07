using AutoSpareParts.Application.Features.Roles.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Roles.Commands.SetPassiveRoleCommand;

public class SetRolePassiveCommandHandler : IRequestHandler<SetPassiveRoleCommandRequest, SetPassiveRoleCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetRolePassiveCommandHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetPassiveRoleCommandResponse> Handle(SetPassiveRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        List<int> defaultRoleIds = new List<int>() { 1, 2, 3 };
        if (!defaultRoleIds.Contains(request.Id))
        {
            AppRole role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role != null)
            {
                if (role.IsActive)
                {
                    role.IsActive = false;
                    role.IsDeleted = true;
                    role.ModifiedTime = DateTime.Now;
                    role.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                    roleResult = await _roleManager.UpdateAsync(role);
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                    foreach (var user in usersInRole)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    if (roleResult.Succeeded)
                    {
                        return new SetPassiveRoleCommandResponse
                        {
                            Result = new Result(ResultStatus.Success, Messages.RoleUpdated)
                        };
                    }
                    return new SetPassiveRoleCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.RoleNotDeleted,roleResult.Errors.ToList())
                    };
                }
                return new SetPassiveRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.RoleNotActive)
                };
            }
            return new SetPassiveRoleCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotFound)
            };
        }
        return new SetPassiveRoleCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleDefaultRole)
        };
        
    }
}