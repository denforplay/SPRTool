using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.API.Requests;
using SPR.Shared.Models.API.Responses;
using SPR.Shared.Models.Group;
using System.Net.Http.Json;

namespace SPR.Client.Communication.Http
{
    public class AuthHttpService : IAuthHttpService
    {
        private readonly HttpClient _authClient;

        public AuthHttpService(HttpClient authClient)
        {
            _authClient = authClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = await _authClient.PostAsJsonAsync($"/user/Login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var signupResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return signupResponse;
            }
            else
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = $"Login response status: {response.StatusCode.ToString()}"
                };
            }
        }

        public async Task<LoginResponse> SignupAsync(SignupRequest signupRequest)
        {
            var response = await _authClient.PostAsJsonAsync($"/user/Signup", signupRequest);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return loginResponse;
        }
    }
}
