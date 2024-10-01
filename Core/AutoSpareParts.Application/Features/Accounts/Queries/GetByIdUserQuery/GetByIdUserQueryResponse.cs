using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByIdUserQuery;

public class GetByIdUserQueryResponse
{
    public IDataResult<UserDto> Result { get; set; }
}