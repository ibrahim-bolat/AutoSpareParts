using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.DeleteEmployeeImageCommand;

public class DeleteEmployeeImageCommandRequest:IRequest<DeleteEmployeeImageCommandResponse>
{
    public int Id { get; set; }
    public string ModifiedByName { get; set; }
}