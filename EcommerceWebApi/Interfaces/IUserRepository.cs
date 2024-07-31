using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.UserDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Interfaces
{
    public interface IUserRepository
    {
      //  Task<User> GetByEmailAsync(string Email);
        Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<UserDto> LoginUserAsync(LoginUserDto loginUserDto);

//        Task<User> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
