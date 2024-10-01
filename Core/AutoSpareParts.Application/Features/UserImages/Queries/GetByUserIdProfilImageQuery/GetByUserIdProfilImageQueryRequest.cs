using MediatR;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdProfilImageQuery;

public class GetByUserIdProfilImageQueryRequest:IRequest<GetByUserIdProfilImageQueryResponse>
{
    public int UserId { get; set; }
}