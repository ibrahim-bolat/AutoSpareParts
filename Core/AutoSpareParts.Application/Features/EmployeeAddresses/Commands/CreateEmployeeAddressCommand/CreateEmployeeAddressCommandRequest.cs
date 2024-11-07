using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.CreateAddressCommand;

public class CreateEmployeeAddressCommandRequest:IRequest<CreateEmployeeAddressCommandResponse>
{
    public EmployeeAddressDto EmployeeAddressDto { get; set; }
}