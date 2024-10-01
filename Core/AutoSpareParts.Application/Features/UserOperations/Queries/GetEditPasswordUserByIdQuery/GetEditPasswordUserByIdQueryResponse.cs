using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetEditPasswordUserByIdQuery;

public class GetEditPasswordUserByIdQueryResponse
{
    public IDataResult<EditPasswordUserDto> Result { get; set; }
}