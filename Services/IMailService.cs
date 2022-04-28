using ERAManagementSystem.Models;

namespace ERAManagementSystem.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}