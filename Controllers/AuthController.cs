using Microsoft.AspNetCore.Mvc;
using BackEnd.Entities;
using BackEnd.Repositories;
using BackEnd.Dto;
using System;

namespace BackEnd.Controllers 
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Hello()
        {
            
            return Ok();
        }
        [HttpPost("Login")]
        // Đăng nhập bằng tên tài khoản
        public ActionResult Login(LoginUserDto userDto)
        {
            User user = userRepository.LoginUser(userDto.Email);
            if(user is null)
            {
                return BadRequest(new {message = "Email Chưa đăng kí tài khoản"});
            }
            if(!BCrypt.Net.BCrypt.Verify(userDto.PassWord, user.PassWord))
            {
                return BadRequest(new {message = "Mật khẩu không chính xác"});
            }
            return Ok(User);
        }
        [HttpPost("Register")]
        public ActionResult Register(CreateUserDto userDto)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                PassWord = BCrypt.Net.BCrypt.HashPassword(userDto.PassWord) // Chuyển đổi thành mật khẩu                
            };
            userRepository.CreateUser(user);
            
            return CreatedAtAction(nameof(Register), new {user = user});
        }

    }
}