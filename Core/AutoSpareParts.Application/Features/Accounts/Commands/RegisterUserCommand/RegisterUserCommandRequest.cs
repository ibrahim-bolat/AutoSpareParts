using AutoSpareParts.Application.Features.Accounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Commands.RegisterUserCommand;

public class RegisterUserCommandRequest:IRequest<RegisterUserCommandResponse>
{
    public RegisterDto RegisterDto { get; set; }
}