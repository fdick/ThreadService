using Microsoft.Extensions.Configuration;
using System.Text.Json;
using ThreadService.Application.Abstractions;

namespace ThreadService.Application.Services
{
    public class KeycloakTokenProvider : ITokenProvider
    {
        private const string TOKEN_ENDPOINT = "http://localhost:8080/realms/main/protocol/openid-connect/token";
        private const string ISSUER = "http://localhost:8080/realms/main";
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public KeycloakTokenProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var parameters = new Dictionary<string, string>
            {
                ["client_id"] = _configuration["Jwt:clientid"]!,
                ["client_secret"] = _configuration["Jwt:secret"]!,
                ["grant_type"] = "client_credentials"
            };

            var response = await _httpClient.PostAsync(
                TOKEN_ENDPOINT,
                new FormUrlEncodedContent(parameters)
            );

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(json);


            return tokenResponse.access_token;
        }

        public class TokenResponse
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
        }


    }
}
