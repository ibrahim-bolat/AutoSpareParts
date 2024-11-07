using MediatR;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeExternalLoginAuthenticationPropertiesQuery;

public class GetEmployeeExternalLoginAuthenticationPropertiesQueryRequest : IRequest<GetEmployeeExternalLoginAuthenticationPropertiesQueryResponse>
{
    public string ProviderName { get; set; }
    public string RedirectUrl { get; set; }
}