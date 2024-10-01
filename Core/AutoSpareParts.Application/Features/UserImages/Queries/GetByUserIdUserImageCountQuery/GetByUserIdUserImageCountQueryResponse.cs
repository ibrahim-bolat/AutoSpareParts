using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserImages.Queries.GetByUserIdUserImageCountQuery;

public class GetByUserIdUserImageCountQueryResponse
{
    public IDataResult<int> Result { get; set; }
}