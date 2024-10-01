using System.Web;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Model;
using AutoSpareParts.Application.Services;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.Application.Features.Accounts.Queries.ForgetPasswordUserQuery;

public class ForgetPasswordUserQueryHandler:IRequestHandler<ForgetPasswordUserQueryRequest,ForgetPasswordUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUrlHelper _urlHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ForgetPasswordUserQueryHandler(UserManager<AppUser> userManager, IEmailService emailService, IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _emailService = emailService;
        _urlHelper = urlHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ForgetPasswordUserQueryResponse> Handle(ForgetPasswordUserQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByEmailAsync(request.ForgetPassDto.Email);
        if (user != null)
        {
            if (user.IsActive)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var sheme = _httpContextAccessor.HttpContext?.Request.Scheme;
                var confirmationLink = _urlHelper.ActionLink("UpdatePassword", "Account",
                    new { area = "Admin", userId = user.Id, token = HttpUtility.UrlEncode(token) }, sheme);
                MailRequest mailRequest = new MailRequest
                {
                    ToMail = request.ForgetPassDto.Email,
                    DisplayName = "Bolat A.Ş.",
                    ConfirmationLink = confirmationLink,
                    MailSubject = "Şifre Güncelleme Talebi",
                    IsBodyHtml = true,
                    MailLinkTitle = "Yeni şifre talebi için tıklayınız"
                };
                bool emailResponse = _emailService.SendEmail(mailRequest);
                if (emailResponse)
                {
                    return new ForgetPasswordUserQueryResponse{
                        Result = new Result(ResultStatus.Success, Messages.UserSuccessSendEmail)
                    };
                }
                return new ForgetPasswordUserQueryResponse{
                    Result = new Result(ResultStatus.Error, Messages.UserErrorSendEmail)
                };
            }
            return new ForgetPasswordUserQueryResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotActive)
            };
        }
        return new ForgetPasswordUserQueryResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotFound)
        };
    }
}