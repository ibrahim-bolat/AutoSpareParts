using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ExternalLoginEmployeeCommand;

public class ExternalLoginEmployeeCommandRequest : IRequest<ExternalLoginEmployeeCommandResponse>
{
    public bool IsPersistent { get; set; }
}