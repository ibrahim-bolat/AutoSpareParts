using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ForgetEmployeePasswordCommand;

public class ForgetEmployeePasswordCommandRequest : IRequest<ForgetEmployeePasswordCommandResponse>
{
    public ForgetEmployeePasswordDto ForgetEmployeePasswordDto { get; set; }
}