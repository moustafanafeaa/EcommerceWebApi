using AutoMapper;
using EcommerceWebApi.Data;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CartDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public CartRepository(AppDbContext context, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetCartDto>> GetAllAsync()
        {
            var carts = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(p => p.Product)
                .Include(u => u.user).AsNoTracking().ToListAsync();
            if(carts == null)
                return null;
            
            var result = _mapper.Map<IEnumerable<GetCartDto>>(carts);
            return result;
        }

        public async Task<ResponseDto> GetByIdAsync(int id)
        {
            var cart =await _context.Carts.Include(c => c.CartItems).ThenInclude(p=>p.Product).Include(u=>u.user).FirstOrDefaultAsync(cart => cart.Id == id);
            if (cart == null)
            {
                return new ResponseDto
                {
                    Message = "No cart with this id.",
                    IsSucceeded = false
                };
            }

            var result = _mapper.Map<GetCartDto>(cart);
            return new ResponseDto
            {
                Model = result,
                IsSucceeded = true
            };
        }

        public async Task<ResponseDto> GetByUserIdAsync(int userId)
        {
            var validUser = await _context.Users.FindAsync(userId);
            if (validUser == null)
            {
                return new ResponseDto
                {
                    Message = "No User with this id.",
                    IsSucceeded = false
                };
            }

            var cart = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(p => p.Product).Include(u => u.user)
                .Where(cart => cart.UserId == userId).ToListAsync();

            if (cart == null || !cart.Any())
            {
                return new ResponseDto
                {
                    Message = "this user don't have any cart",
                    IsSucceeded = false
                };
            }

            var result = _mapper.Map<IEnumerable<GetCartDto>>(cart);
            return new ResponseDto
            {
                Model = result,
                IsSucceeded = true
            };
        }

        public async Task<ResponseDto> AddCartAsync(AddCartDto cartDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == cartDto.UserId);
            if(user == null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "no user with this id"
                };
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(u => u.UserId == cartDto.UserId);
            if(cart != null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "This user already have a cart"
                };
            }
            
            var newCart = _mapper.Map<Cart>(cartDto);
            
           await _context.Carts.AddAsync(newCart);
           await _context.SaveChangesAsync();

            var res = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(p => p.Product)
                .Include(u => u.user)
                .FirstOrDefaultAsync(cart => cart.Id == newCart.Id);

            var result = _mapper.Map<GetCartDto>(res);
            
            return new ResponseDto
            {
                Model = result,
                IsSucceeded = true
            };
        }

        public async Task<ResponseDto> DeleteCartAsync(int id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(cart => cart.Id == id);
            if (cart == null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "No cart with this id."
                };
            }
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                Message = $"Cart with id:{id} is deleted successfully",
                Model = cart,
                IsSucceeded = true
            };
        }

    }
}
