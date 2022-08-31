using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain_Layer.Helpers
{
    public class JwtService
    {
        private readonly IConfiguration Configuration;
        private string secureKey = "BuiVietQuangCheckPass";
        public string jwt { get; set; }

        public JwtService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"], // Nơi được xác thực bằng JWT
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}