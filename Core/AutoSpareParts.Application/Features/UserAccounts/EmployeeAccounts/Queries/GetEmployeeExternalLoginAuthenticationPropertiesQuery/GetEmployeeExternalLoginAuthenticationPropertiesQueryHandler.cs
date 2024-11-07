using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeExternalLoginAuthenticationPropertiesQuery;

public class GetEmployeeExternalLoginAuthenticationPropertiesQueryHandler : IRequestHandler<GetEmployeeExternalLoginAuthenticationPropertiesQueryRequest, GetEmployeeExternalLoginAuthenticationPropertiesQueryResponse>
{

    private readonly SignInManager<Employee> _employeeSignInManager;

    public GetEmployeeExternalLoginAuthenticationPropertiesQueryHandler(SignInManager<Employee> employeeSignInManager)
    {
        _employeeSignInManager = employeeSignInManager;
    }

    public Task<GetEmployeeExternalLoginAuthenticationPropertiesQueryResponse> Handle(GetEmployeeExternalLoginAuthenticationPropertiesQueryRequest request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ProviderName) && !string.IsNullOrEmpty(request.RedirectUrl))
        {
            AuthenticationProperties properties = _employeeSignInManager.ConfigureExternalAuthenticationProperties(request.ProviderName.Trim(), request.RedirectUrl);
            return Task.FromResult(new GetEmployeeExternalLoginAuthenticationPropertiesQueryResponse
            {
                Result = new DataResult<AuthenticationProperties>(ResultStatus.Success, properties)
            });
        }
        return Task.FromResult(new GetEmployeeExternalLoginAuthenticationPropertiesQueryResponse
        {
            Result = new DataResult<AuthenticationProperties>(ResultStatus.Error, Messages.AuthenticationPropertiesNotFound, null)
        });
    }
}