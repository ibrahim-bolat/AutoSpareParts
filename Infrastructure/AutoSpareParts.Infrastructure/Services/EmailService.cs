using System.Net;
using System.Net.Mail;
using AutoSpareParts.Application.Models;
using AutoSpareParts.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutoSpareParts.Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptionsMonitor<MailSettings> options, ILogger<EmailService> logger)
    {
        _mailSettings = options.CurrentValue;
        _logger = logger;
    }
    public bool SendEmail(MailRequest mailRequest)
    {

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_mailSettings.Mail, mailRequest.DisplayName, System.Text.Encoding.UTF8);
        mailMessage.To.Add(new MailAddress(mailRequest.ToMail));

        mailMessage.Subject = mailRequest.MailSubject;
        mailMessage.IsBodyHtml = mailRequest.IsBodyHtml;
        mailMessage.Body = $"<a target=\"_blank\" href=\"{mailRequest.ConfirmationLink}\">{mailRequest.MailLinkTitle}</a>";

        SmtpClient client = new SmtpClient();
        client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
        client.Port = _mailSettings.Port;
        client.Host = _mailSettings.Host;
        client.EnableSsl = _mailSettings.UseSsl;

        try
        {
            client.Send(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{mailRequest.ToMail} e-mail adresine mail gönderirken hata oluþtu. {Environment.NewLine}{ex}");
            Console.WriteLine(ex);
            return false;
        }
    }
}