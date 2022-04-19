using Microsoft.AspNetCore.Mvc;
using BackEnd.Entities;
using BackEnd.Repositories;
using BackEnd.Dto;
using System;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace BackEnd.Controllers 
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService jwtService;
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository, JwtService jwtService )
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
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
            String jwt = jwtService.generate(user.Id); // Lưu phía sever
            

            // Phía Client sẽ nếu đăng nhập thành công thì Cookie sẽ trả về True
            Response.Cookies.Append("jwt", jwt , new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new {
                message = "thành công"
            });
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
        [HttpGet("Login")]
        public ActionResult Login()
        {
            try
            {
                String jwt = Request.Cookies["jwt"];
            
                var token = jwtService.Verify(jwt);

                Guid UserId = Guid.Parse(token.Issuer);

                var user = userRepository.GetById(UserId);

                return Ok(user);
            }
            catch (System.Exception)
            {
                
                return Unauthorized();
            }
        }

    }
}