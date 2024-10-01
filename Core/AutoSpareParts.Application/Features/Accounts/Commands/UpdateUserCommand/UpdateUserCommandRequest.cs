using AutoSpareParts.Application.Features.Accounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Commands.UpdateUserCommand;

public class UpdateUserCommandRequest:IRequest<UpdateUserCommandResponse>
{
    public UserDto UserDto { get; set; }
}