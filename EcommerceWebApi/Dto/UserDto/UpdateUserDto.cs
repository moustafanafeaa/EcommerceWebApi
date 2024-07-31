using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApi.Dto.UserDto
{
    public class UpdateUserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
