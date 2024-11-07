using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.CreateEmployeeImageCommand;

public class CreateEmployeeImageCommandRequest:IRequest<CreateEmployeeImageCommandResponse>
{
    public CreateEmployeeImageDto CreateEmployeeImageDto { get; set; }
    public string CreatedByName { get; set; }
}