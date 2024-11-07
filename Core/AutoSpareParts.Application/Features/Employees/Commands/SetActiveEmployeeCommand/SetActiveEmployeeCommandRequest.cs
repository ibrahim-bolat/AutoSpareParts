using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Commands.SetActiveEmployeeCommand;

public class SetActiveEmployeeCommandRequest:IRequest<SetActiveEmployeeCommandResponse>
{
    public string Id { get; set; }
}