namespace EcommerceWebApi.Models
{
    public class ProductColors
    {
        public string Color { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
