using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListQuery;

public class GetIPAddressListQueryRequest:IRequest<GetIPAddressListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}