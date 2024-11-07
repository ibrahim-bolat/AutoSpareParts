using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeePasswordCommand;

public class UpdateEmployeePasswordCommandRequest : IRequest<UpdateEmployeePasswordCommandResponse>
{
    public UpdateEmployeePasswordDto UpdateEmployeePasswordDto { get; set; }
    public string EmployeeId { get; set; }
    public string Token { get; set; }
}