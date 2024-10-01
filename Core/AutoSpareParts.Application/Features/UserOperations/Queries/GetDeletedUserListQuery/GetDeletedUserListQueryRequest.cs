using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryRequest:IRequest<GetDeletedUserListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}