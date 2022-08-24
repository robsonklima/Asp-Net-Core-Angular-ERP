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
            message.From = new MailAddress(Constants.EMAIL_TESTE.Username);
            message.To.Add(new MailAddress(email.EmailDestinatario));
            message.Subject = email.Assunto;
            message.Subject = email.Assunto;
            message.Body = email.Corpo;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Constants.EMAIL_TESTE.Username, Constants.EMAIL_TESTE.Password);
            client.Host = Constants.EMAIL_TESTE.Host;
            client.Port = (int)Constants.EMAIL_TESTE.Port;
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

                    HttpResponseMessage response = await httpClient.GetAsync($"{Constants.EMAIL_TESTE.ApiUri}v1.0/users/${Constants.EMAIL_TESTE.ClientID}/messages");
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
                    .Create(Constants.EMAIL_TESTE.ClientID)
                    .WithClientSecret(Constants.EMAIL_TESTE.ClientSecret)
                    .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, Constants.EMAIL_TESTE.Instance, Constants.EMAIL_TESTE.Tenant)))
                    .Build();

            string[] scopes = new string[] { $"{Constants.EMAIL_TESTE.ApiUri}.default" };

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