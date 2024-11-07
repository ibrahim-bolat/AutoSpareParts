using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByEmployeeIdQuery;

public class GetEmployeeImageListByEmployeeIdQueryRequest:IRequest<GetEmployeeImageListByEmployeeIdQueryResponse>
{
    public int EmployeeId { get; set; }
}