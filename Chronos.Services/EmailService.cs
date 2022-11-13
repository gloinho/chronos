using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Chronos.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;

        public EmailService(EmailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task Send(string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailSettings.MailSender));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = html };

            var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(
                _mailSettings.ServerSmtp,
                _mailSettings.ServerPortSmtp,
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(_mailSettings.UserSmtp, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
