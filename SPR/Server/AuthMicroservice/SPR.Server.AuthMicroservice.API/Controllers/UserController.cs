using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SPR.Server.AuthMicroservice.Domain.Interfaces;
using SPR.Server.AuthMicroservice.Domain.Models;
using SPR.Shared.Models.API.Requests;
using SPR.Shared.Models.API.Responses;
using SPR.Shared.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPR.Server.AuthMicroservice.API.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;
        public UserController(IUserRepository userRepository, JwtTokenConfiguration jwtTokenConfiguration)
        {
            _userRepository = userRepository;
            _jwtTokenConfiguration = jwtTokenConfiguration;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse();
            var findedUser = await _userRepository.ReadFirstByConditionAsync(x => x.Username == loginRequest.Username && x.Password == loginRequest.Password);
            if (findedUser is not null)
            {
                var jwtToken = GenerateToken(findedUser);
                loginResponse.IsSuccess = true;
                loginResponse.Data = new UserModel
                {
                    JwtToken = jwtToken
                };
                return loginResponse;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Signup([FromBody] SignupRequest signupRequest)
        {
            var loginResponse = new LoginResponse();
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = signupRequest.Name,
                Surname = signupRequest.Surname,
                Password = signupRequest.Password,
                Username = signupRequest.Username
            };

            _userRepository.Add(user);
            var jwtToken = GenerateToken(user);
            loginResponse.IsSuccess = true;
            loginResponse.Data = new UserModel
            {
                JwtToken = jwtToken
            };

            return loginResponse;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfiguration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                //new Claim(ClaimTypes.Role, )
            };

            var token = new JwtSecurityToken(_jwtTokenConfiguration.Issuer,
                _jwtTokenConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}