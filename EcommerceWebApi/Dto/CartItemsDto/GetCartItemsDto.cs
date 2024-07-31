using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Dto.CartItemsDto
{
    public class GetCartItemsDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }
       
    }
}
