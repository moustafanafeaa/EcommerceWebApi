using EcommerceWebApi.Dto.CartDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _cartRepository.GetAllAsync();

            return carts == null ? NotFound("No carts") : Ok(carts);
        }

        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            return !cart.IsSucceeded ? NotFound(cart.Message) : Ok(cart.Model);
        }

        [HttpGet("UserId/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var cart = await _cartRepository.GetByUserIdAsync(id);
            return !cart.IsSucceeded ? NotFound(cart.Message) : Ok(cart.Model);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cartRepository.DeleteCartAsync(id);
            return !result.IsSucceeded ? NotFound(result.Message) : Ok(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(AddCartDto cartDto)
        {
            var result = await _cartRepository.AddCartAsync(cartDto);
            return !result.IsSucceeded ? BadRequest(result.Message) : Ok(result.Model);

        }
    }
}