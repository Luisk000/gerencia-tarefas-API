using Azure.Core;
using GerenciaTarefas.API.Models;
using System.Text.Json;

namespace GerenciaTarefas.API.Services
{
    public class OAuthService : IOauthService
    {
        private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        private readonly string authUrl = configuration["Authentication:auth_url"]!;
        private readonly HttpClient _httpClient;

        public OAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAcess(string client_id, string client_secret)
        {
            var response = new HttpResponseMessage();
            var collection = new List<KeyValuePair<string, string>>
                {
                    new ("grant_type", "client_credentials"),
                    new ("client_id", client_id),
                    new ("client_secret", client_secret),
                    new ("scope", "api://08b23737-7017-4d00-9e68-3aa3be3ec63e/.default")
                };

            response = await SendRequest(collection);

            string auth = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(auth);

            string token = doc.RootElement.GetProperty("access_token").GetString()!;
            return token;
        }

        private async Task<HttpResponseMessage> SendRequest(List<KeyValuePair<string, string>> collection)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, authUrl);
            request.Content = new FormUrlEncodedContent(collection);

            var response = await _httpClient.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            
            return response;
        }
    }
}
