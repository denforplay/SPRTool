using SPR.Shared.Models.API.Requests;
using SPR.Shared.Models.API.Responses;

namespace SPR.Client.Abstractions.Http
{
    public interface IAuthHttpService
    {
        Task<LoginResponse> SignupAsync(SignupRequest signupRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}