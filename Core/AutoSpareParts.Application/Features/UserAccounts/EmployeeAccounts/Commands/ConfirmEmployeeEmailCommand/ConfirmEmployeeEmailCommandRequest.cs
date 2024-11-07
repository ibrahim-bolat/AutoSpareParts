using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ConfirmEmployeeEmailCommand;

public class ConfirmEmployeeEmailCommandRequest : IRequest<ConfirmEmployeeEmailCommandResponse>
{
    public string Email { get; set; }
    public string Token { get; set; }
}