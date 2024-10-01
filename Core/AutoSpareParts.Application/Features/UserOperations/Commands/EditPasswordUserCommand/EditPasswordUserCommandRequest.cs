using AutoSpareParts.Application.Features.UserOperations.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Commands.EditPasswordUserCommand;

public class EditPasswordUserCommandRequest:IRequest<EditPasswordUserCommandResponse>
{
    public EditPasswordUserDto EditPasswordUserDto { get; set; }
}