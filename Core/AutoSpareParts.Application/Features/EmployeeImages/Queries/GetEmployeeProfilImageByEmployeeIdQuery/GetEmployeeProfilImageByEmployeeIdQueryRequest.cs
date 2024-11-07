using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeProfilImageByEmployeeIdQuery;

public class GetEmployeeProfilImageByEmployeeIdQueryRequest:IRequest<GetEmployeeProfilImageByEmployeeIdQueryResponse>
{
    public int EmployeeId { get; set; }
}