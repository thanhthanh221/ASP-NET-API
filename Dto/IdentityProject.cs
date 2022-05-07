using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Dto
{
    // Dữ liệu nhập luồng trả luồng với Client
    public class UsersDto
    {
        [Required]
        public string Name { get; set; }
 
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
 
        [Required]
        public string Password { get; set; }
    }
}