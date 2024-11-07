using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.RegisterEmployeeCommand;

public class RegisterEmployeeCommandRequest : IRequest<RegisterEmployeeCommandResponse>
{
    public RegisterEmployeeDto RegisterEmployeeDto { get; set; }
}