using Newtonsoft.Json;

namespace SPR.Server.AuthMicroservice.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}