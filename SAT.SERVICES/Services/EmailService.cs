using MailKit.Net.Smtp;
using MimeKit;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EmailService : IEmailService
    {
        public void Enviar(Email email)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(email.NomeRemetente, email.EmailRemetente);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(email.NomeDestinatario, email.EmailDestinatario);
            message.To.Add(to);

            message.Subject = email.Assunto;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = email.Corpo;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(Constants.SMTP_HOST, Constants.SMTP_PORT, MailKit.Security.SecureSocketOptions.Auto);
            client.Authenticate(Constants.SMTP_USER, Constants.SMTP_PASSWORD);

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}