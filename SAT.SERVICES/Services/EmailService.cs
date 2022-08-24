using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;
using SAT.SERVICES.Interfaces;
using System.Globalization;
using SAT.MODELS.Entities.Constants;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Linq;
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

        public async Task ObterEmailsAsync()
        {
            var token = await ObterTokenAsync();

            try
            {
                if (token != null)
                {
                    HttpClient httpClient = new();
                    var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                    if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }
                    defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await httpClient.GetAsync($"{Constants.OUTLOOK_API_URI}v1.0/users/${Constants.OUTLOOK_CLIENT_ID}/messages");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        JObject result = JsonConvert.DeserializeObject(json) as JObject;
                    }
                    else
                    {
                        string content = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter emails do Outlook {ex.Message}");
            }
        }

        public async Task<string> ObterTokenAsync()
        {
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder
                    .Create(Constants.OUTLOOK_CLIENT_ID)
                    .WithClientSecret(Constants.OUTLOOK_CLIENT_SECRET)
                    .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, Constants.OUTLOOK_INSTANCE, Constants.OUTLOOK_TENANT)))
                    .Build();

            string[] scopes = new string[] { $"{Constants.OUTLOOK_API_URI}.default" };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                return result.AccessToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Token da Microsoft {ex.Message}");
            }
        }
    }
}