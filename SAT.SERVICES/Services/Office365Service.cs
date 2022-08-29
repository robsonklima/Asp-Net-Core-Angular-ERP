using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class Office365Service : IOffice365Service
    {
        public async Task<Office365Email> ObterEmailsAsync(string token, string clientID)
        {
            try
            {
                HttpClient httpClient = new();
                var headers = httpClient.DefaultRequestHeaders;

                if (headers.Accept == null || !headers.Accept.Any(m => m.MediaType == "application/json"))
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage res = await httpClient.GetAsync($"{Constants.OFFICE_365_CONFIG.ApiUri}v1.0/users/{clientID}/messages");
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

        public async Task DeletarEmailAsync(string token, string clientID, string emailID)
        {
            try
            {
                HttpClient httpClient = new();
                var headers = httpClient.DefaultRequestHeaders;

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

        public async Task<string> ObterTokenAsync()
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