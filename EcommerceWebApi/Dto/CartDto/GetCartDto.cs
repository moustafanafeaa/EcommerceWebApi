using EcommerceWebApi.Dto.CartItemsDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Dto.CartDto
{
    public class GetCartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<GetCartItemsDto> CartItemDtos { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }
    }
}
