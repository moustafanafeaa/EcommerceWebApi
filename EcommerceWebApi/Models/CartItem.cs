using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Models
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int ProductId  { get; set;}
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
 
}
