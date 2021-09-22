using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SAT.SERVICES.Services
{
    public class EmailService
    {
        private readonly string smtp_server = "zimbragd.perto.com.br";
        private readonly string smtp_server_user_aplicacao = "aplicacao.sat@perto.com.br";
        private readonly string smtp_server_user_password = "S@aPlic20(v";

        public bool EnviaEmail(string strMailFrom, string strMailTo, string strCC = null, string strBCC = null, string strSubject = null, string strBody = null, string attachment = null, string strMailFormat = null)
        {
            MailMessage message = new();

            try
            {
                string removeDuplicadosFrom = string.Join(';', strMailFrom.Trim().Split(';').Distinct());

                message.From = new MailAddress(removeDuplicadosFrom);

                if (!string.IsNullOrWhiteSpace(strMailTo))
                {
                    string removeDuplicados = string.Join(';', strMailTo.Trim().Split(';').Distinct());
                    foreach (string email in removeDuplicados.Split(';'))
                    {
                        message.To.Add(email.Trim().Replace(";", ""));
                    }
                }

                if (!string.IsNullOrWhiteSpace(strBCC))
                {
                    string removeDuplicados = string.Join(';', strBCC.Trim().Split(';').Distinct());
                    foreach (string email in removeDuplicados.Split(';'))
                    {
                        message.Bcc.Add(email.Trim().Replace(";", ""));
                    }
                }

                if (!string.IsNullOrWhiteSpace(strCC))
                {
                    string removeDuplicados = string.Join(';', strCC.Trim().Split(';').Distinct());
                    foreach (string email in removeDuplicados.Split(';'))
                    {
                        message.CC.Add(email.Trim().Replace(";", ""));
                    }
                }

                Array arrAttachment;

                message.Subject = strSubject;
                message.Body = strBody;
                message.BodyEncoding = System.Text.Encoding.UTF8;

                if (attachment != "")
                {
                    arrAttachment = attachment.Split(Convert.ToChar(","));

                    for (int i = 0; i <= arrAttachment.Length - 1; i++)
                        message.Attachments.Add(new Attachment(arrAttachment.GetValue(i).ToString()));
                }

                if (strMailFormat == "text")
                    message.IsBodyHtml = false;
                else
                    message.IsBodyHtml = true;

                SmtpClient client = new(this.smtp_server, 587);
                client.EnableSsl = true;
                NetworkCredential cred = new(this.smtp_server_user_aplicacao, this.smtp_server_user_password);
                client.Credentials = cred;

                client.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Serviço de email - Não foi possível enviar o email: {strMailFrom}, {strMailTo}, {strSubject}, {strBody} - Exception: {ex.InnerException.Message}");
                return false;
            }
        }
    }
}
