using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CartItemsDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Interfaces
{
    public interface ICartItemRepository
    {
        Task<ResponseDto> GetAllCartsItemsAsync();
        Task<ResponseDto> GetByCartIdAsync(int CartId);
        Task<ResponseDto> AddItemToCartAsync(CartItemDto cartItemDto);
        Task<ResponseDto> RemoveItemFromCartAsync(int productId,int cartId);
        Task<ResponseDto> UpdateItemFromCart(CartItemDto cartItemDto);
        
    }
}
