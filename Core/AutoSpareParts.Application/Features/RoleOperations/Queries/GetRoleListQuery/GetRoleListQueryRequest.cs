using AutoSpareParts.Application.DTOs.Common;
using MediatR;

namespace AutoSpareParts.Application.Features.RoleOperations.Queries.GetRoleListQuery;

public class GetRoleListQueryRequest:IRequest<GetRoleListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}