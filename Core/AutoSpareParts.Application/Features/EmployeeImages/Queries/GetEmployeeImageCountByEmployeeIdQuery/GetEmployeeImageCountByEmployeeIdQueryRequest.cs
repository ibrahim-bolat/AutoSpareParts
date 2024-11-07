using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageCountByEmployeeIdQuery;

public class GetEmployeeImageCountByEmployeeIdQueryRequest:IRequest<GetEmployeeImageCountByEmployeeIdQueryResponse>
{
    public int EmployeeId { get; set; }
}