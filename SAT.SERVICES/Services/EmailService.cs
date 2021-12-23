using MailKit.Net.Smtp;
using MimeKit;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EmailService : IEmailService
    {
        public void Enviar(
            string nomeRemetente,
            string emailRemetente,
            string nomeDestinatario,
            string emailDestinatario,
            string assunto,
            string corpo
        )
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(nomeRemetente, emailRemetente);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(nomeDestinatario, emailDestinatario);
            message.To.Add(to);

            message.Subject = assunto;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = corpo;
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