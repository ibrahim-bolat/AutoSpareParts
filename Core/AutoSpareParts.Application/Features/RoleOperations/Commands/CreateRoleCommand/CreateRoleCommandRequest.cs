using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.RoleOperations.Commands.CreateRoleCommand;

public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
{
    public RoleDto RoleDto{ get; set; }
}