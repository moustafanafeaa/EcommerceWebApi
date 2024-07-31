using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.Order;
using EcommerceWebApi.Dto.OrderItemDto;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<ResponseDto> GetAllAsync();//
        Task<ResponseDto> GetAsync(int id);//
        Task<ResponseDto> GetUserOrdersAsync(int userId);
        Task<ResponseDto> UpdateAsync(int  id,Order orderDto);
        Task<ResponseDto> DeleteAsync(int id);
        Task<ResponseDto> CreateAsync(AddOrderDto OrderDto);

    }
}
