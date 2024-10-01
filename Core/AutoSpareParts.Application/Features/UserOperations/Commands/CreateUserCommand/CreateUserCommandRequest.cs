using AutoSpareParts.Application.Features.UserOperations.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Commands.CreateUserCommand;

public class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
{
    public CreateUserDto CreateUserDto { get; set; }
}