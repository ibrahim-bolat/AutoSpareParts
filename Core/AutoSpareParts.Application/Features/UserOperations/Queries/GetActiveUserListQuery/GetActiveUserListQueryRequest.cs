using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetActiveUserListQuery;

public class GetActiveUserListQueryRequest:IRequest<GetActiveUserListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}