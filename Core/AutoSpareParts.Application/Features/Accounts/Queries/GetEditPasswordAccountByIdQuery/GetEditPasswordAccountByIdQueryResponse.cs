using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;

public class GetEditPasswordAccountByIdQueryResponse
{
    public IDataResult<EditPasswordAccountDto> Result { get; set; }
}