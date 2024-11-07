using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.DeleteEmployeeAddressCommand;

public class DeleteEmployeeAddressCommandResponse
{
    public IDataResult<EmployeeAddressDetailDto> Result { get; set; }
}