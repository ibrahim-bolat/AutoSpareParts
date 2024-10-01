using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.RoleOperations.Commands.UpdateRoleCommand;

public class UpdateRoleCommandRequest:IRequest<UpdateRoleCommandResponse>
{
    public RoleDto RoleDto{ get; set; }
}