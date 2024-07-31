using EcommerceWebApi.Dto.CategoryDto;
using EcommerceWebApi.Dto.ProductColorDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Dto.Product
{
    public class GetProductDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CategoryDto.CategoryDto Category { get; set; }

        public ICollection<GetProductColorDto> ProductColors { get; set; }

    }
}
