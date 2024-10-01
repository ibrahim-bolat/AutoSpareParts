using System.Security.Claims;
using AutoMapper;
using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Accounts.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        AppUser user = await _userManager.FindByIdAsync(request.UserDto.Id.ToString());
        if (user != null)
        {
            string tempUserName = user.UserName;
            if (user.IsActive)
            {
                user = _mapper.Map(request.UserDto, user);
                user.NormalizedEmail = _userManager.NormalizeEmail(request.UserDto.Email);
                user.NormalizedUserName = _userManager.NormalizeName(request.UserDto.UserName);
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new UpdateUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdated, result.Errors.ToList())
                    };
                }
                result = await _userManager.UpdateSecurityStampAsync(user);
                if (!result.Succeeded)
                {
                    return new UpdateUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdateSecurityStamp, result.Errors.ToList())
                    };
                }
                string userIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                if (!String.IsNullOrEmpty(userIdentityName) && userIdentityName.Equals(tempUserName))
                {
                    await _signInManager.RefreshSignInAsync(user);
                }
                return new UpdateUserCommandResponse
                {
                    Result = new DataResult<UserDto>(ResultStatus.Success, Messages.UserUpdated, request.UserDto)
                };

            }
            return new UpdateUserCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserNotActive)
                };
        }
        return new UpdateUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotFound)
            };
    }
}