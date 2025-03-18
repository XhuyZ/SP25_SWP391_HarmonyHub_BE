using Domain.DTOs.Requests;

namespace Service.Interfaces;

public interface IEmailService
{
    Task SendEmail(MailRequest mailRequest);
}