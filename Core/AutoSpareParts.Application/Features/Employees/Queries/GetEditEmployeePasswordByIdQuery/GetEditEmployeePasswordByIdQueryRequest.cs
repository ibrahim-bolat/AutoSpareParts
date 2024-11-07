using MediatR;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEditEmployeePasswordByIdQuery;

public class GetEditEmployeePasswordByIdQueryRequest:IRequest<GetEditEmployeePasswordByIdQueryResponse>
{
    public string Id { get; set; }
}