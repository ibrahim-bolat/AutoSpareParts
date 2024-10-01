using AutoSpareParts.Application.Model;

namespace AutoSpareParts.Application.Services;

public interface IEmailService
{
    bool SendEmail(MailRequest mailRequest);
}