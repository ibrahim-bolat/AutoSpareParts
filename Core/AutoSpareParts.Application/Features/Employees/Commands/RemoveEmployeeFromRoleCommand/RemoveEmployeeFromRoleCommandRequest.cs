using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Commands.RemoveUserFromRoleCommand;

public class RemoveEmployeeFromRoleCommandRequest : IRequest<RemoveEmployeeFromRoleCommandResponse>
{
    public string UserId { get; set; }
    public string RoleId { get; set; }
}