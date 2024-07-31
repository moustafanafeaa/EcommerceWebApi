using AutoMapper;
using EcommerceWebApi.Data;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.Order;
using EcommerceWebApi.Dto.OrderItemDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseDto> GetAllAsync()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).AsNoTracking().ToListAsync();        
            if(orders is null || orders.Count == 0)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "no any order"
                };
            }
            return new ResponseDto
            {
                IsSucceeded = true,
                Model = orders
            };
        }

        public async Task<ResponseDto> GetAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "no order with this id"
                };
            }
            return new ResponseDto
            {
                IsSucceeded = true,
                Model = order
            };
        }

        public Task<ResponseDto> GetUserOrdersAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> CreateAsync(AddOrderDto orderDto)
        {
            var newOrder = _mapper.Map<Order>(orderDto);
            if(newOrder != null)
            {
                await _context.Orders.AddAsync(newOrder);
                await _context.OrderItems.AddRangeAsync(newOrder.OrderItems);
                await _context.SaveChangesAsync();

                var createdOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(n => n.Id == newOrder.Id);

                return new ResponseDto { IsSucceeded = true, Model = newOrder };
            }
            return new ResponseDto
            {
                IsSucceeded = false,
                Message = "   "
            };

        }

        public async Task<ResponseDto> DeleteAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "no order with this id"
                };
            }
            _context.Orders.Remove(order);
            return new ResponseDto
            {
                IsSucceeded = true,
                Model = order,
                Message = $"order with id: {id} is deleted.."
            };
        }

        public Task<ResponseDto> UpdateAsync(int id, Order orderDto)
        {
            throw new NotImplementedException();
        }
    }
}
