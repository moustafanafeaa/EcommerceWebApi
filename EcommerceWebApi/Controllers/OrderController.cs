using EcommerceWebApi.Dto.Order;
using EcommerceWebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();
            return !orders.IsSucceeded ? NotFound(orders.Message) : Ok(orders.Model);
        }
        [HttpGet("OrderId/{Id}")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            var order = await _orderRepository.GetAsync(Id);
            return !order.IsSucceeded ? NotFound(order.Message) : Ok(order.Model);
        }



        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDto addOrderDto)
        {
            var order = await _orderRepository.CreateAsync(addOrderDto);
            return !order.IsSucceeded ? NotFound(order.Message) : Ok(order.Model);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderRepository.DeleteAsync(id);
            return !result.IsSucceeded ? NotFound(result.Message) : Ok(result.Message);
        }

    } 

}
