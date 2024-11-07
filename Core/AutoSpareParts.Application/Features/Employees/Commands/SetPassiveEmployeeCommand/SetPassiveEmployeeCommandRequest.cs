using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Commands.SetPassiveEmployeeCommand;

public class SetPassiveEmployeeCommandRequest:IRequest<SetDeletedUserCommandResponse>
{
    public string Id { get; set; }
}