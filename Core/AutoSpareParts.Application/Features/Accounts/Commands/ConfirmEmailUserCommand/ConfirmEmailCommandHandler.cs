using System.Web;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Accounts.Commands.ConfirmEmailUserCommand;

public class ConfirmEmailUserCommandHandler:IRequestHandler<ConfirmEmailUserCommandRequest,ConfirmEmailUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ConfirmEmailUserCommandResponse> Handle(ConfirmEmailUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new ConfirmEmailUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotFound)
            };
        }
        var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(request.Token));
        if (result.Succeeded)
        {
            return new ConfirmEmailUserCommandResponse{
                Result = new Result(ResultStatus.Success, Messages.UserConfirmEmail)
            };
        }
        return new ConfirmEmailUserCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotConfirmEmail,result.Errors.ToList())
        };
    }
}