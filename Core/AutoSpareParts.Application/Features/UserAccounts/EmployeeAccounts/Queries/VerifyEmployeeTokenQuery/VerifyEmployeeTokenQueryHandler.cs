using System.Web;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.VerifyEmployeeTokenQuery;

public class VerifyEmployeeTokenQueryHandler : IRequestHandler<VerifyEmployeeTokenQueryRequest, VerifyEmployeeTokenQueryResponse>
{
    private readonly UserManager<Employee> _employeeManager;

    public VerifyEmployeeTokenQueryHandler(UserManager<Employee> employeeManager)
    {
        _employeeManager = employeeManager;

    }

    public async Task<VerifyEmployeeTokenQueryResponse> Handle(VerifyEmployeeTokenQueryRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByIdAsync(request.EmployeeId);
        if (employee != null)
        {
            string verifyToken = HttpUtility.UrlDecode(request.Token);
            bool result = await _employeeManager.VerifyUserTokenAsync(employee, _employeeManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", verifyToken);
            if (result)
            {
                return new VerifyEmployeeTokenQueryResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.SuccessVerifyEmployeeToken)
                };
            }
            return new VerifyEmployeeTokenQueryResponse
            {
                Result = new Result(ResultStatus.Error, Messages.ErrorVerifyEmployeeToken)
            };
        }
        return new VerifyEmployeeTokenQueryResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}