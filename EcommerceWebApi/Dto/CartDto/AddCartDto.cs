using EcommerceWebApi.Dto.CartItemsDto;

namespace EcommerceWebApi.Dto.CartDto
{
    public class AddCartDto
    {
        public int UserId { get; set; }
        public ICollection<CartItemDto>? CartItems { get; set; }
    }


}
