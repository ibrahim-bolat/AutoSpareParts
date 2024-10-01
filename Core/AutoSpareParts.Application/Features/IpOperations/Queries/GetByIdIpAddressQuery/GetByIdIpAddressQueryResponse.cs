using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.IpOperations.Queries.GetByIdIpAddressQuery;

public class GetByIdIpAddressQueryResponse
{
    public IDataResult<IpDto> Result { get; set; }
}