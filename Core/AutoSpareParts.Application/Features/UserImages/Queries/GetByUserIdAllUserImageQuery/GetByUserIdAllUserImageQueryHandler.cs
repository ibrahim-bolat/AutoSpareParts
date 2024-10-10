using AutoMapper;
using AutoSpareParts.Application.Features.UserImages.Constants;
using AutoSpareParts.Application.Features.UserImages.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;

public class GetByUserIdAllUserImageQueryHandler:IRequestHandler<GetByUserIdAllUserImageQueryRequest,GetByUserIdAllUserImageQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByUserIdAllUserImageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByUserIdAllUserImageQueryResponse> Handle(GetByUserIdAllUserImageQueryRequest request, CancellationToken cancellationToken)
    {
        var userImages = await _unitOfWork.UserImages.GetAllAsync(predicate:ui=>ui.UserId==request.UserId && ui.IsActive);
        var userImagesViewDtoList = _mapper.Map<IList<UserImageDto>>(userImages);
        if (userImages.Count > -1)
        {
            return new GetByUserIdAllUserImageQueryResponse{
                Result = new DataResult<IList<UserImageDto>>(ResultStatus.Success,userImagesViewDtoList)
            };
        }
        return new GetByUserIdAllUserImageQueryResponse{
            Result = new DataResult<IList<UserImageDto>>(ResultStatus.Error, Messages.UserImageNotFound,null)
        };
    }
}