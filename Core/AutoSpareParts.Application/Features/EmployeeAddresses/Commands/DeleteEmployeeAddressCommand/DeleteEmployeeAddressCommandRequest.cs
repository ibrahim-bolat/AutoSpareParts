using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.DeleteEmployeeAddressCommand;

public class DeleteEmployeeAddressCommandRequest:IRequest<DeleteEmployeeAddressCommandResponse>
{
    public int Id { get; set; }
    public string ModifiedByName { get; set; }
}