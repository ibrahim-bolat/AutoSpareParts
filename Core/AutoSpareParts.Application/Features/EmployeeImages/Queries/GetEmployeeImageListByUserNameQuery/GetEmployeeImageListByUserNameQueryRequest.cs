using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByUserNameQuery;

public class GetEmployeeImageListByUserNameQueryRequest : IRequest<GetEmployeeImageListByUserNameQueryResponse>
{
    public string UserName { get; set; }
}