
using AutoSpareParts.Application.Wrappers.Abstract;
using Microsoft.AspNetCore.Authentication;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetExternalLoginAuthenticationPropertiesQuery;

public class GetExternalLoginAuthenticationPropertiesQueryResponse
{
    public IDataResult<AuthenticationProperties> Result { get; set; }
}