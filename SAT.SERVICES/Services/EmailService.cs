using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;
using SAT.SERVICES.Interfaces;
using System.Globalization;
using SAT.MODELS.Entities.Constants;
using System.Net.Http;
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

        public async Task<Office365Email> ObterEmailsAsync(EmailConfig emailConfig)
        {
            var token = await ObterTokenAsync();
            var emails = new Office365Email();

            try
            {
                if (token != null)
                {
                    HttpClient httpClient = new();
                    var headers = httpClient.DefaultRequestHeaders;

                    if (headers.Accept == null || !headers.Accept.Any(m => m.MediaType == "application/json"))
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage res = await httpClient.GetAsync($"{Constants.OFFICE_365_CONFIG.ApiUri}v1.0/users/{emailConfig.ClientId}/messages");
                    if (res.IsSuccessStatusCode)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        emails = Newtonsoft.Json.JsonConvert.DeserializeObject<Office365Email>(json);
                    }
                    else
                    {
                        var content = await res.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao obter emails do Outlook {content}");
                    }
                }

                return emails;
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
                .Create(Constants.OFFICE_365_CONFIG.ClientID)
                .WithClientSecret(Constants.OFFICE_365_CONFIG.ClientSecret)
                .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, Constants.OFFICE_365_CONFIG.Instance, Constants.OFFICE_365_CONFIG.Tenant)))
                .Build();

            string[] scopes = new string[] { $"{Constants.OFFICE_365_CONFIG.ApiUri}.default" };

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