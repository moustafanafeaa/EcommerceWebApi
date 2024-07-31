using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Address { get; set; }
        public DateOnly? ShippingDate { get; set; }
        public DateOnly? ArrivalDate { get; set;}
        public int? UserId { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
        
        
        
    }
}
