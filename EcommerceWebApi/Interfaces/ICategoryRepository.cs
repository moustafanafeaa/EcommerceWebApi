using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CategoryDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ResponseDto> GetAllAsync();
        Task<ResponseDto> GetByIdAsync(int Id);
        Task<ResponseDto> GetByNameAsync(string Name);
        Task<ResponseDto> GetCategoryWithProductsAsync(int Id);

        Task<ResponseDto> DeleteByIdAsync(int Id);
        Task<ResponseDto> DeleteByNameAsync(string name);
        Task<ResponseDto> UpdateAsync(int Id, CategoryDto dto);
        Task<ResponseDto> AddAsync(CategoryDto dto);
    }
}
