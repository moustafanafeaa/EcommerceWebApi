using AutoMapper;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.UserDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public UsersController(IMapper mapper, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var UserDto= await _userRepository.RegisterUserAsync(registerUserDto);

            if (UserDto is null)
                return BadRequest("User already exists.");

            var token = _tokenRepository.GenerateJwtToken(UserDto);
            return Ok(new { Token = token});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _userRepository.LoginUserAsync(loginUserDto);
            
            if (result is not null){
                var token = _tokenRepository.GenerateJwtToken(result);
                return Ok(new { Token = token });
            }
            return Unauthorized("Email or Password are Incorrect");
            
        }
        










        //[HttpPut]
        //public async Task<IActionResult> UpdateProfile(UpdateUserDto updateUserDto)
        //{
        //    //var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        //    //if (string.IsNullOrEmpty(userEmail))
        //    //    return BadRequest("User Not Found");

        //    var UpdatedUser = _userRepository.UpdateUserAsync(updateUserDto);
        //    return Ok(UpdatedUser);

        //}
    }
}
