using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.LoginEmployeeCommand;

public class LoginEmployeeCommandRequest : IRequest<LoginEmployeeCommandResponse>
{
    public LoginEmployeeDto LoginEmployeeDto { get; set; }
}