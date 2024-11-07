using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.EditEmployeePasswordCommand;

public class EditEmployeePasswordCommandRequest : IRequest<EditEmployeePasswordCommandResponse>
{
    public EditEmployeePasswordDto EditPasswordAccountDto { get; set; }
}