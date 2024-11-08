using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.EditEmployeeAccountPasswordCommand;

public class EditEmployeeAccountPasswordCommandRequest : IRequest<EditEmployeeAccountPasswordCommandResponse>
{
    public EditEmployeeAccountPasswordDto EditPasswordAccountDto { get; set; }
}