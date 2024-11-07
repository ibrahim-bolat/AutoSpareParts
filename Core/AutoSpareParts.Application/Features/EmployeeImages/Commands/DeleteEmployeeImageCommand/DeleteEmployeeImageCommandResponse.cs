
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.DeleteEmployeeImageCommand;

public class DeleteEmployeeImageCommandResponse
{
    public IDataResult<EmployeeImageDto> Result { get; set; }
}