using AutoSpareParts.Application.Wrappers.Abstract;
using Microsoft.AspNetCore.Authentication;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeExternalLoginAuthenticationPropertiesQuery;

public class GetEmployeeExternalLoginAuthenticationPropertiesQueryResponse
{
    public IDataResult<AuthenticationProperties> Result { get; set; }
}