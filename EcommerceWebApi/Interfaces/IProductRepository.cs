using EcommerceWebApi.Dto.Product;
using EcommerceWebApi.Dto.ProductDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<GetProductDto> GetByNameAsync(string name);
        Task<GetProductDto> UpdateAsync(int id, UpdateProductDto Dto);
        Task<Product> DeleteAsync(int id);
        Task<Product> CreateAsync(UpdateProductDto createProductDto);

        Task<IEnumerable<GetProductDto>> GetByCategoryNameAsync(string categoryName);

    }
}
