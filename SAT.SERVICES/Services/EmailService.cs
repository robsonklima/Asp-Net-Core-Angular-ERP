using System;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using System.Net;
using System.Net.Mail;
using SAT.MODELS.Entities;

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
            message.From = new MailAddress(Constants.EMAIL_TESTE_CONFIG.Username);
            message.To.Add(new MailAddress(email.EmailDestinatario));
            message.Subject = email.Assunto;
            message.Subject = email.Assunto;
            message.Body = email.Corpo;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Constants.EMAIL_TESTE_CONFIG.Username, Constants.EMAIL_TESTE_CONFIG.Password);
            client.Host = Constants.OFFICE_365_CONFIG.Host;
            client.Port = (int)Constants.OFFICE_365_CONFIG.Port;
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