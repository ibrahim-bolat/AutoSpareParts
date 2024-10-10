using AutoSpareParts.Application.Models;

namespace AutoSpareParts.Application.Services;

public interface IEmailService
{
    bool SendEmail(MailRequest mailRequest);
}