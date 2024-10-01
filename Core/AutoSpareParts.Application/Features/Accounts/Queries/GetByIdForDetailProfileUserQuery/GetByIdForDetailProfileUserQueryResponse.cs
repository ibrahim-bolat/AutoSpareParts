
using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByIdForDetailProfileUserQuery;

public class GetByIdForDetailProfileUserQueryResponse
{
    public IDataResult<UserDetailDto> Result { get; set; }
}