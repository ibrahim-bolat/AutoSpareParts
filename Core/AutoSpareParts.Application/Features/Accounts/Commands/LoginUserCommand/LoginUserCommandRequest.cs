using AutoSpareParts.Application.Features.Accounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Commands.LoginUserCommand;

public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
{
    public LoginDto LoginDto { get; set; }
}