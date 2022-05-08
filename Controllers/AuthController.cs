using Microsoft.AspNetCore.Mvc;
using BackEnd.Entities;
using BackEnd.Repositories;
using BackEnd.Dto;
using System;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers 
{
    // Chỉnh Theo quy trình của Identity
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtService jwtService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController( JwtService jwtService,
                                UserManager<ApplicationUser> userManager ,
                                SignInManager<ApplicationUser> signInManager )
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("Login")]
        // Đăng nhập bằng email
        public async Task<ActionResult> LoginAsync([FromForm] LoginUserDto LoginDto)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(LoginDto.Email);
            if(user is null)
            {
                return BadRequest(new {message = "Email Chưa đăng kí tài khoản"});
            }
            // Đăng nhập tài khoản
             Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, LoginDto.PassWord, false, true);

            if(!result.Succeeded){
                return Unauthorized(new {message = "Đăng nhập thất bại"});
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
        public async Task<ActionResult> RegisterAsync([FromForm] CreateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = userDto.Name,
                    Email = userDto.Email
                };
                // IdentityResult => userManager.CreateAsync() => Tạo mới một người dùng 
                IdentityResult result = await userManager.CreateAsync(appUser, userDto.Password);
                // Nếu đăng kí thành công
                if (result.Succeeded)
                    return CreatedAtAction(nameof(RegisterAsync), new {appUser = appUser});
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            // Yêu cầu không hợp lệ
            return BadRequest();
        }
        [HttpGet("Login")]
        public async Task<ActionResult> Login()
        {
            try
            {
                String jwt = Request.Cookies["jwt"];
            
                JwtSecurityToken token = jwtService.Verify(jwt);

                Guid UserId = Guid.Parse(token.Issuer);

                ApplicationUser user = await userManager.FindByIdAsync(UserId.ToString());

                return Ok(user);
            }
            catch (System.Exception)
            {
                // Trả về 401
                return Unauthorized();
            }
        }
        [HttpGet("Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            
            return Ok(new {
                message = "Thành Công"
            });
        }

    }
}