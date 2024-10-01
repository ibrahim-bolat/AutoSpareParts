using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.IpOperations.Queries.GetIpAddressListQuery;

public class GetIpAddressListQueryRequest:IRequest<GetIpAddressListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}