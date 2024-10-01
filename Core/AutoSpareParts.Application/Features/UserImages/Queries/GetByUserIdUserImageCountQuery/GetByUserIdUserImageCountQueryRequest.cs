using MediatR;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdUserImageCountQuery;

public class GetByUserIdUserImageCountQueryRequest:IRequest<GetByUserIdUserImageCountQueryResponse>
{
    public int UserId { get; set; }
}