using AutoSpareParts.Application.Features.Employees.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Commands.CreateEmployeeCommand;

public class CreateEmployeeCommandRequest:IRequest<CreateEmployeeCommandResponse>
{
    public CreateEmployeeDto CreateEmployeeDto { get; set; }
}