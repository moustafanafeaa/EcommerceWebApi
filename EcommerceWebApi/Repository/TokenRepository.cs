using EcommerceWebApi.Dto.UserDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceWebApi.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JwtOptions _jwtOptions;

        public TokenRepository(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GenerateJwtToken(UserDto userDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDto.UserName),
                    new Claim(ClaimTypes.Email, userDto.Email),
                    new Claim(ClaimTypes.Role, string.Join(",", userDto.Roles)),
                    new Claim(ClaimTypes.MobilePhone, userDto.PhoneNumber)
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.LifeTime),
                SigningCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
