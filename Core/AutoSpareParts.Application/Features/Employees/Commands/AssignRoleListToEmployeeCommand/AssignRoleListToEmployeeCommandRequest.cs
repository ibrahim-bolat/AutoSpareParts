using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Commands.AssignRoleListToEmployeeCommand;

public class AssignRoleListToEmployeeCommandRequest:IRequest<AssignRoleListToEmployeeCommandResponse>
{
    public string Id { get; set; }
    public List<int> RoleIds { get; set; }
}