using AutoMapper;
using EcommerceWebApi.Data;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CartItemsDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public CartItemRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    

        public async Task<ResponseDto> GetAllCartsItemsAsync()
        {
            var cartItems = await _context.CartItems.Include(x=>x.Cart).Include(p=>p.Product).ToListAsync();
            var result = _mapper.Map<IEnumerable<GetCartItemsDto>>(cartItems);
            return new ResponseDto
            {
                Model = result
            };
        }
        
        public async Task<ResponseDto> GetByCartIdAsync(int CartId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == CartId);
            if (cart == null)
            {
                return new ResponseDto { IsSucceeded = false, Message = "No cart with this id" };
            }
            var cartItem = await _context.CartItems.Include(c=>c.Cart).Include(c=>c.Product).Where(c => c.CartId == CartId).ToListAsync();
            if (cartItem == null || cartItem.Count == 0)
            {
                return new ResponseDto { IsSucceeded = false, Message = "This cart is empty" };
            }
            var result = _mapper.Map<IEnumerable<GetCartItemsDto>>(cartItem);
            return new ResponseDto
            {
                Model = result,
                IsSucceeded = true
            };
        }

        
        public async Task<ResponseDto> AddItemToCartAsync(CartItemDto cartItemDto)
        {
            var cartItemExists = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartItemDto.CartId && ci.ProductId == cartItemDto.ProductId);

            if (cartItemExists != null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "The cart item already exists"
                };
            }   

            var cart = _mapper.Map<CartItem>(cartItemDto);
            

            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
            var addedCart =await _context.CartItems.Include(c => c.Cart).Include(c => c.Product).FirstOrDefaultAsync(c => c.CartId == cart.CartId);
            var newCart = _mapper.Map<GetCartItemsDto>(addedCart);
            return new ResponseDto { Model = newCart, IsSucceeded = true };

        }

        public async Task<ResponseDto> RemoveItemFromCartAsync(int CartId,int ProductId)
        {
            var cartItemExists = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == CartId && ci.ProductId == ProductId);

            if (cartItemExists == null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "no items in this cart"
                };
            }
            _context.CartItems.Remove(cartItemExists);
            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                IsSucceeded = true,
                Message = "item deleted"
            };

        }

        public async Task<ResponseDto> UpdateItemFromCart(CartItemDto cartItemDto)
        {
            var cartItemExists = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartItemDto.CartId && ci.ProductId == cartItemDto.ProductId);

            if (cartItemExists == null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "no items in this cart"
                };
            }
       
                cartItemExists.Quantity =  cartItemDto.Quantity;
                _context.CartItems.Update(cartItemExists);
                await _context.SaveChangesAsync();
            
            
            return new ResponseDto()
            {
                IsSucceeded = true,
                Message = "Cart item is updated"
            };
        }
    }
}
