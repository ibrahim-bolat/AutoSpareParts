using AutoSpareParts.Application.Features.UserImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;

public class GetByUserIdAllUserImageQueryResponse
{
    public IDataResult<IList<UserImageDto>> Result { get; set; }
}