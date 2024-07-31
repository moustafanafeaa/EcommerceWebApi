namespace EcommerceWebApi.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
    }
}
