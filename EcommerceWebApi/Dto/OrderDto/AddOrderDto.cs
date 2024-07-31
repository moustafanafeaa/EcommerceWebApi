using EcommerceWebApi.Dto.OrderItemDto;
using EcommerceWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Dto.Order
{
    public class AddOrderDto
    {     
        public int UserId { get; set; }
        public string? Address { get; set; }
        public ICollection<AddOrderItemDto>? addOrderItemDtos { get; set; }
    }
}
