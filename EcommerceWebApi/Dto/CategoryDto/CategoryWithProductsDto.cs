using EcommerceWebApi.Dto.Product;

namespace EcommerceWebApi.Dto.CategoryDto
{
    public class CategoryWithProductsDto
    {
        public string Name { get; set; }
        public List<ProductsInCategoryDto> Products { get; set; }
    }
}
