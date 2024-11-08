using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeeAccountPasswordCommand;

public class UpdateEmployeeAccountPasswordCommandRequest : IRequest<UpdateEmployeeAccountPasswordCommandResponse>
{
    public UpdateEmployeePasswordDto UpdateEmployeePasswordDto { get; set; }
    public string EmployeeId { get; set; }
    public string Token { get; set; }
}