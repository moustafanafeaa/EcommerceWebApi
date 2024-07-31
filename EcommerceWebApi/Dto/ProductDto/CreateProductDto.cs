using EcommerceWebApi.Dto.ProductColorDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApi.Dto.ProductDto
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductColores {  get; set; }
    }
}
