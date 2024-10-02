using AutoSpareParts.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Accounts.Queries.LogoutUserQuery;

public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQueryRequest>
{
    private readonly SignInManager<AppUser> _signInManager;

    public LogoutUserQueryHandler(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task Handle(LogoutUserQueryRequest request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}