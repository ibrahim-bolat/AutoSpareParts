using MediatR;

namespace AutoSpareParts.Application.Features.UserImages.Commands.SetProfilImageCommand;

public class SetProfilImageCommandRequest:IRequest<SetProfilImageCommandResponse>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ModifiedByName { get; set; }
}