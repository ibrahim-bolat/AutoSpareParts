using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeProfileDetailByIdQuery;

public class GetEmployeeProfileDetailByIdRequest : IRequest<GetEmployeeProfileDetailByIdResponse>
{
    public int Id { get; set; }
}