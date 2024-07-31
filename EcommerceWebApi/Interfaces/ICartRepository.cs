using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CartDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<GetCartDto>> GetAllAsync();
        Task<ResponseDto> GetByIdAsync(int id);
        Task<ResponseDto> GetByUserIdAsync(int userId);
        Task<ResponseDto> DeleteCartAsync(int id);
        Task<ResponseDto> AddCartAsync(AddCartDto addCartDto);

    }
}
