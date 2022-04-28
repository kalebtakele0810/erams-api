using ERAManagementSystem.Models;
using ERAManagementSystem.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ERAManagementSystem.Services;

public class MailService: IMailService
{
    private readonly MailSetting _mailSetting;

    public MailService(IOptions<MailSetting> mailSetting)
    {
        _mailSetting = mailSetting.Value;
    }


    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSetting.Mail);
        email.To.Add(MailboxAddress.Parse(mailRequest.To));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        if (mailRequest.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSetting.Mail, _mailSetting.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}