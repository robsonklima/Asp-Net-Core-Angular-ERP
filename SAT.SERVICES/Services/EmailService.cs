using System;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using System.Net;
using System.Net.Mail;
using SAT.MODELS.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Linq;
using System.Net.Mime;

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

            foreach (string enderencoEmail in email.EmailDestinatarios)
            {
                if (!string.IsNullOrWhiteSpace(enderencoEmail)) {
                    message.To.Add(new MailAddress(enderencoEmail));    
                }
            }
            
            message.Subject = email.Assunto;
            message.Subject = email.Assunto;
            message.Body = email.Corpo;
            message.IsBodyHtml = true;
            
            if((email.Anexos != null ) && (email.Anexos.Any()))
            {
                email.Anexos.ForEach(anexo =>
                {
                    message.Attachments.Add(new Attachment(anexo, MediaTypeNames.Application.Octet));
                });
            }

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

        public async Task<Office365Email> ObterEmailsAsync(string clientID)
        {
            try
            {
                HttpClient httpClient = new();
                var headers = httpClient.DefaultRequestHeaders;
                var token = await ObterTokenAsync();

                if (headers.Accept == null || !headers.Accept.Any(m => m.MediaType == "application/json"))
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage res = await httpClient.GetAsync($"{Constants.OFFICE_365_CONFIG.ApiUri}v1.0/users/{clientID}/mailFolders/inbox/messages");
                if (res.IsSuccessStatusCode)
                {
                    var json = await res.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Office365Email>(json);
                }
                else
                {
                    var content = await res.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao obter emails do Outlook {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter emails do Outlook {ex.Message}");
            }
        }

        public async Task DeletarEmailAsync(string clientID, string emailID)
        {
            try
            {
                HttpClient httpClient = new();
                var headers = httpClient.DefaultRequestHeaders;
                var token = await ObterTokenAsync();

                if (headers.Accept == null || !headers.Accept.Any(m => m.MediaType == "application/json"))
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage del = await httpClient
                    .DeleteAsync($"{Constants.OFFICE_365_CONFIG.ApiUri}v1.0/users/{clientID}/messages/{emailID}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar email do Outlook {ex.Message}");
            }
        }

        private async Task<string> ObterTokenAsync()
        {
            try
            {
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                    .Create(Constants.OFFICE_365_CONFIG.ClientID)
                    .WithClientSecret(Constants.OFFICE_365_CONFIG.ClientSecret)
                    .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, Constants.OFFICE_365_CONFIG.Instance, Constants.OFFICE_365_CONFIG.Tenant)))
                    .Build();

                string[] scopes = new string[] { $"{Constants.OFFICE_365_CONFIG.ApiUri}.default" };

                AuthenticationResult result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                return result.AccessToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Token da Microsoft {ex.Message}");
            }
        }
    }
}