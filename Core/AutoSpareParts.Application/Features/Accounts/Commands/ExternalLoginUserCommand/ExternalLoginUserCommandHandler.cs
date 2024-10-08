using System.Security.Claims;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Accounts.Commands.ExternalLoginUserCommand;

public class ExternalLoginUserCommandHandler : IRequestHandler<ExternalLoginUserCommandRequest, ExternalLoginUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public ExternalLoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ExternalLoginUserCommandResponse> Handle(ExternalLoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        ExternalLoginInfo loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            return new ExternalLoginUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.LoginInfoNotFound)
            };
        }
        AppUser loginUser = await _userManager.FindByEmailAsync(loginInfo.Principal.FindFirstValue(ClaimTypes.Email));
        if (loginUser!=null && loginUser.IsDeleted)
        {
            return new ExternalLoginUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotActive)
            };
        }
        SignInResult loginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider,
            loginInfo.ProviderKey, request.IsPersistent);
        if (loginResult.Succeeded)
        {
            return new ExternalLoginUserCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.UserLoggedIn)
                };
        }
        AppUser user = new AppUser
        {
            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),           
            FirstName = loginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = loginInfo.Principal.FindFirstValue(ClaimTypes.Surname)
        };
        IdentityResult createResult = await _userManager.CreateAsync(user);
        if (createResult.Succeeded)
        {
            IdentityResult addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
            if (addLoginResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, request.IsPersistent);
                return new ExternalLoginUserCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.UserLoggedIn)
                };
            }
            return new ExternalLoginUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotLoggedIn,addLoginResult.Errors.ToList())
            };
        }
        return new ExternalLoginUserCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.UserNotAdded,createResult.Errors.ToList())
        };
    }
}