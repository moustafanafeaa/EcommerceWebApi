using EcommerceWebApi.Dto.CartItemsDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepositiry;
        private readonly IProductRepository _productRepositiry;
        private readonly ICartRepository _cartRepository;
        public CartItemController(ICartItemRepository cartItemRepositiry, IProductRepository productRepositiry, ICartRepository cartRepository)
        {
            _cartItemRepositiry = cartItemRepositiry;
            _productRepositiry = productRepositiry;
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cartItemRepositiry.GetAllCartsItemsAsync();
            return Ok(result.Model);
        }

        [HttpGet("CartId/{cartId}")]
        public async Task<IActionResult> GetByCartId(int cartId)
        {
            var result = await _cartItemRepositiry.GetByCartIdAsync(cartId);
            return result.IsSucceeded == false ? NotFound(result.Message) : Ok(result.Model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(CartItemDto cartItemDto)
        {
            var result = await _cartItemRepositiry.AddItemToCartAsync(cartItemDto);
            return result.IsSucceeded == false ? BadRequest(result.Message) : Ok(result.Model);
        }

        [HttpDelete("{cartId},{ProductId}")]
        public async Task<IActionResult> DeleteItemFromCart(int cartId, int ProductId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var product = await _productRepositiry.GetByIdAsync(ProductId);
            if (product == null || !cart.IsSucceeded )
                return BadRequest("no cart or product with this id");

            var result = await _cartItemRepositiry.RemoveItemFromCartAsync(cartId,ProductId);
            return !result.IsSucceeded ? NotFound(result.Message) : Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(CartItemDto cartItemDto)
        {
            
            var cart = await _cartRepository.GetByIdAsync(cartItemDto.CartId);
            var product = await _productRepositiry.GetByIdAsync(cartItemDto.ProductId);
            if (product == null || !cart.IsSucceeded)
                return BadRequest("no cart or product with this id");

            var updatedCartItem = await _cartItemRepositiry.UpdateItemFromCart(cartItemDto);
            return !updatedCartItem.IsSucceeded ? BadRequest(updatedCartItem.Message) : Ok(updatedCartItem.Message);
        }
    }

}
