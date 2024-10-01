using AutoSpareParts.Application.Features.UserImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdProfilImageQuery;

public class GetByUserIdProfilImageQueryResponse
{
    public IDataResult<UserImageDto> Result { get; set; }
}