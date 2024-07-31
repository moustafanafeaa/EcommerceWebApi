using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Models
{
    public class OrderItem
    {
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
