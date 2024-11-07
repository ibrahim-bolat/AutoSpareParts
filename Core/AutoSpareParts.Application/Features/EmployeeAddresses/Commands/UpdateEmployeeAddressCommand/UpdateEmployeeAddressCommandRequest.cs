using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.UpdateEmployeeAddressCommand;

public class UpdateEmployeeAddressCommandRequest:IRequest<UpdateEmployeeAddressCommandResponse>
{
    public EmployeeAddressDto EmployeeAddressDto { get; set; }
    public string ModifiedByName { get; set; }
}