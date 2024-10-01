using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.Features.Accounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Commands.EditPasswordAccountCommand;

public class EditPasswordAccountCommandRequest:IRequest<EditPasswordAccountCommandResponse>
{
    public EditPasswordAccountDto EditPasswordAccountDto { get; set; }
}