using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.IpOperations.Queries.GetIpAddressListQuery;

public class GetIpAddressListQueryResponse
{
    public IDataResult<DatatableResponseDto<IpListDto>> Result { get; set; }
}