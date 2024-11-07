using AutoSpareParts.Application.Features.Employees.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Commands.EditEmployeePasswordCommand;

public class EditEmployeePasswordCommandRequest:IRequest<EditEmployeePasswordCommandResponse>
{
    public EditEmployeePasswordDto EditEmployeePasswordDto { get; set; }
}