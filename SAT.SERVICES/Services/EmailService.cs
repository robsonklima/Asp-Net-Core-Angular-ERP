using System;
using System.Net;
using System.Net.Mail;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EmailService : IEmailService
    {
        public void Enviar(Email email)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(Constants.SMTP_USER);
            message.To.Add(new MailAddress(email.EmailDestinatario));
            message.Subject = email.Assunto;
            message.Subject = email.Assunto;
            message.Body = email.Corpo;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Constants.SMTP_USER, Constants.SMTP_PASSWORD);
            client.Host = Constants.SMTP_HOST;
            client.Port = Constants.SMTP_PORT;
            client.EnableSsl = true;

            try
            {
                client.Send(message);
                client.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}