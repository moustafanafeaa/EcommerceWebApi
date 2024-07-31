using AutoMapper;
using EcommerceWebApi.Data;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.UserDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace EcommerceWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> RegisterUserAsync(RegisterUserDto RegisterDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == RegisterDto.Email))
                return null;
             
            CreatePasswordHash(RegisterDto.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = _mapper.Map<User>(RegisterDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            if(user.UserName == "admin") 
                user.Roles = new List<string> { "Admin", "User" };
            else
                user.Roles = new List<string> { "User" };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email == loginUserDto.Email);
            if (user == null || !VerifyPasswordHash(loginUserDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    
        //public async Task<User> UpdateUserAsync(UpdateUserDto updateUserDto)
        //{
        //    var user = await GetByEmailAsync(updateUserDto.Email);
        //    if (user == null)
        //        return null;

        //    var newUser = _mapper.Map<User>(updateUserDto);

        //    _context.Users.Update(newUser);
        //    await _context.SaveChangesAsync();

        //    return newUser;
        //}

        //public async Task<User> GetByEmailAsync(string Email)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(e => e.Email ==  Email);
        //}
    }
}
