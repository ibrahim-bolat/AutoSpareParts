using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListQuery;

public class GetIPAddressListQueryResponse
{
    public IDataResult<DatatableResponseDto<IPListDto>> Result { get; set; }
}