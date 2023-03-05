using SPR.Shared.Models.API.Interfaces;
using SPR.Shared.Models.User;

namespace SPR.Shared.Models.API.Responses
{
    public class LoginResponse : IResponse<UserModel>
    {
        public bool IsSuccess { get; set; } = false;
        public UserModel Data { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}