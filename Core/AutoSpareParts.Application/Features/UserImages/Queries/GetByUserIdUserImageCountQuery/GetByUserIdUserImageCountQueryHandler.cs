using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdUserImageCountQuery;

public class GetByUserIdUserImageCountQueryHandler:IRequestHandler<GetByUserIdUserImageCountQueryRequest,GetByUserIdUserImageCountQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByUserIdUserImageCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetByUserIdUserImageCountQueryResponse> Handle(GetByUserIdUserImageCountQueryRequest request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.UserImages.CountAsync(predicate:x => x.UserId == request.UserId && x.IsActive);
        return new GetByUserIdUserImageCountQueryResponse{
            Result = new DataResult<int>(ResultStatus.Success,count)
        };
    }
}