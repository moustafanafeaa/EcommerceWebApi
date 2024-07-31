using EcommerceWebApi.Dto.UserDto;

namespace EcommerceWebApi.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> GenerateJwtToken(UserDto userDto);
    }
}
