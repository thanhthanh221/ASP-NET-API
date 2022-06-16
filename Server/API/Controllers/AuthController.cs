using Microsoft.AspNetCore.Mvc;
using BackEnd.Dto;
using System;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Domain_Layer.Entities.Identity;

namespace BackEnd.Controllers 
{
    // Chỉnh Theo quy trình của Identity
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly JwtService jwtService;
        private readonly SignInManager<ApplicationUser> signInManager;
        public IConfiguration Configuration { get; }

        public AuthController( JwtService jwtService,
                                UserManager<ApplicationUser> userManager ,
                                SignInManager<ApplicationUser> signInManager ,
                                RoleManager<ApplicationRole> roleManager )
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpPost("Login")]
        // Đăng nhập bằng email
        public async Task<ActionResult> LoginAsync([FromForm] LoginUserDto LoginDto)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(LoginDto.Email);
            if(user is null || !( await userManager.CheckPasswordAsync(user, LoginDto.PassWord)))
            {
                return Unauthorized(new { message = "Email Chưa đăng kí tài khoản  Hoặc TK MK không chính xác"});
            }
            
            var userRoles = await userManager.GetRolesAsync(user);
            
            // Những thứ được gửi đi
            var authClaims = new List<Claim> // Tạo Claim Liên Quan User
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)      // Claim ở Role
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            
            // Chuyển thành token
            var token = jwtService.GetToken(authClaims);
            jwtService.jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new {
                token = jwtService.jwt,
                expiration = token.ValidTo,
                message = "thành công",
                Obj_user = new {
                    name = user.UserName,
                    role = user.Roles != null ? user.Roles : null,
                }
            });
        }
        [HttpPost("RegisterBuyer")]
        public async Task<IActionResult> RegisterSeller([FromForm] CreateUserDto userDto)
        {
            ApplicationUser useCheckEmailExists = await userManager.FindByEmailAsync(userDto.Email);

            ApplicationUser useCheckNameExists = await userManager.FindByNameAsync(userDto.Name); 

            if(useCheckEmailExists != null || useCheckNameExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                { 
                    Status = "Error", 
                    Message = "Tài Khoản đã tồn tại" 
                });
            }
            ApplicationUser appUser = new ApplicationUser
                {   
                    UserName = userDto.Name,
                    Email = userDto.Email
                };
                // IdentityResult => userManager.CreateAsync() => Tạo mới một người dùng 
            IdentityResult result = await userManager.CreateAsync(appUser, userDto.Password);
            // Nếu trong CSDl vẫn chưa có Role này
            if(await roleManager.FindByNameAsync("Buyer") == null) 
            {
                IdentityResult resultRole = await roleManager.CreateAsync(new ApplicationRole() { Name = "Buyer" });
            }
            await userManager.AddToRoleAsync(appUser, (await roleManager.FindByNameAsync("Buyer")).Name);
                  

            return Ok(new { Status = "Thành Công", Message = "Tạo Thành công một người bán!" });
        }
        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync([FromForm] CreateUserDto userDto)
        {
            ApplicationUser useCheckEmailExists = await userManager.FindByEmailAsync(userDto.Email);

            ApplicationUser useCheckNameExists = await userManager.FindByNameAsync(userDto.Name); 

            if(useCheckEmailExists != null || useCheckNameExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                { 
                    Status = "Error", 
                    Message = "Tài Khoản đã tồn tại" 
                });
            }

            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {   
                    UserName = userDto.Name,
                    Email = userDto.Email
                };
                // IdentityResult => userManager.CreateAsync() => Tạo mới một người dùng 
                IdentityResult result = await userManager.CreateAsync(appUser, userDto.Password);

                var userRole = await userManager.GetRolesAsync(appUser);
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
    }
    
}