using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListQuery;

public class GetIpAddressListQueryRequest:IRequest<GetIpAddressListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}