using Domain.DTOs.Requests;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Service.Exceptions;
using Service.Interfaces;
using Service.Settings;

namespace Service.Implementations;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmail(MailRequest mailRequest)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_emailSettings.Username));
        message.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        message.Subject = mailRequest.Subject;
        message.Body = new TextPart(TextFormat.Html) { Text = mailRequest.Body };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(message);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}