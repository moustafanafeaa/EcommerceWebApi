using System.Text.Json.Serialization;

namespace EcommerceWebApi.Dto.UserDto
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string? Message { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }

        [JsonIgnore]
        public string? Role { get; set; }

    }
}
