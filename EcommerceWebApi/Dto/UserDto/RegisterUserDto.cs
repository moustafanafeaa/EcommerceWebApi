using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApi.Dto
{
    public class RegisterUserDto
    {
        [Required,EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public  string UserName { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required,Compare("Password")]
        public string ConfirmPassword { get; set; }=string.Empty;

        public string? Address {  get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;

    }
}
