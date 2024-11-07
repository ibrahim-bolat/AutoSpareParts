using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.UpdateEmployeeCommand;

public class UpdateUserCommandRequest : IRequest<UpdateEmployeeCommandResponse>
{
    public EmployeeDto EmployeeDto { get; set; }
}