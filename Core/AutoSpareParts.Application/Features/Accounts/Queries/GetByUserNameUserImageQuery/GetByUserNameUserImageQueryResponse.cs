using AutoSpareParts.Application.Features.UserImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByUserNameUserImageQuery;

public class GetByUserNameUserImageQueryResponse
{
    public IDataResult<List<UserImageDto>> Result { get; set; }
    
}