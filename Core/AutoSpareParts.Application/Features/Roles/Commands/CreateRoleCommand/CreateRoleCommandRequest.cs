using AutoSpareParts.Application.Features.Roles.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Commands.CreateRoleCommand;

public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
{
    public RoleDto RoleDto{ get; set; }
}