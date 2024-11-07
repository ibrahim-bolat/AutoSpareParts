using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.SetEmployeeProfilImageCommand;

public class SetEmployeeProfilImageCommandRequest:IRequest<SetEmployeeProfilImageCommandResponse>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string ModifiedByName { get; set; }
}