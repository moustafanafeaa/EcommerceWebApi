namespace EcommerceWebApi.Dto.OrderItemDto
{
    public class AddOrderItemDto
    {
        public int OrderId { get; set; }
        public int ProductId {  get; set; }
        public int Quantity { get; set; }
    }
}
