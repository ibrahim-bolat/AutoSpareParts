using AutoSpareParts.Application.Features.Roles.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Roles.Commands.UpdateRoleCommand;

public class UpdateRoleCommandRequest:IRequest<UpdateRoleCommandResponse>
{
    public RoleDto RoleDto{ get; set; }
}