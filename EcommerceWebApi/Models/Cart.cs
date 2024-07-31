using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApi.Models
{
    public class Cart
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User user { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }
}
