﻿namespace EcommerceWebApi.Dto.UserDto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
