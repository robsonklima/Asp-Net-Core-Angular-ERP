using System.IO;
using System.Linq;
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

            var destinatarios = email.EmailDestinatario.Split(',').Select(i => i.Trim()).ToList();
            InternetAddressList recipients = new InternetAddressList();
            recipients.AddRange(destinatarios.Select(i => new MailboxAddress("", i)));
            message.To.AddRange(recipients);

            if (!string.IsNullOrWhiteSpace(email.NomeCC) && !string.IsNullOrWhiteSpace(email.EmailCC))
            {
                MailboxAddress cc = new MailboxAddress(email.NomeCC, email.EmailCC);
                message.Cc.Add(cc);
            }

            message.Subject = email.Assunto;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = email.Corpo;

            if (email.Anexos.Any())
            {
                email.Anexos.ForEach(file =>
                {
                    var attachment = new MimePart("application/pdf")
                    {
                        Content = new MimeContent(File.OpenRead(file)),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(file)
                    };

                    bodyBuilder.Attachments.Add(attachment);
                });
            };

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