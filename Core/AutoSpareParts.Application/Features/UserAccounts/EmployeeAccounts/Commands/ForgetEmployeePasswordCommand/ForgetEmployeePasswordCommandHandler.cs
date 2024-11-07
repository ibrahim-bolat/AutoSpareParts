using System.Web;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Constants;
using AutoSpareParts.Application.Models;
using AutoSpareParts.Application.Services;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Commands.ForgetEmployeePasswordCommand;

public class ForgetPasswordUserQueryHandler : IRequestHandler<ForgetEmployeePasswordCommandRequest, ForgetEmployeePasswordCommandResponse>
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly IEmailService _emailService;
    private readonly IUrlHelper _urlHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ForgetPasswordUserQueryHandler(UserManager<Employee> employeeManager, IEmailService emailService, IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor)
    {
        _employeeManager = employeeManager;
        _emailService = emailService;
        _urlHelper = urlHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ForgetEmployeePasswordCommandResponse> Handle(ForgetEmployeePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeManager.FindByEmailAsync(request.ForgetEmployeePasswordDto.Email);
        if (employee != null)
        {
            if (employee.IsActive)
            {
                var token = await _employeeManager.GeneratePasswordResetTokenAsync(employee);
                var sheme = _httpContextAccessor.HttpContext?.Request.Scheme;
                var confirmationLink = _urlHelper.ActionLink("UpdateEmployeePassword", "EmployeeAccount", new { area = "Admin", employeeId = employee.Id, token = HttpUtility.UrlEncode(token) }, sheme);
                MailRequest mailRequest = new MailRequest
                {
                    ToMail = request.ForgetEmployeePasswordDto.Email,
                    DisplayName = "Bolat A.Ş.",
                    ConfirmationLink = confirmationLink,
                    MailSubject = "Şifre Güncelleme Talebi",
                    IsBodyHtml = true,
                    MailLinkTitle = "Yeni şifre talebi için tıklayınız"
                };
                bool emailResponse = _emailService.SendEmail(mailRequest);
                if (emailResponse)
                {
                    return new ForgetEmployeePasswordCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.SuccessSendEmployeeEmail)
                    };
                }
                return new ForgetEmployeePasswordCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.ErrorSendEmployeeEmail)
                };
            }
            return new ForgetEmployeePasswordCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeNotActive)
            };
        }
        return new ForgetEmployeePasswordCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.EmployeeNotFound)
        };
    }
}