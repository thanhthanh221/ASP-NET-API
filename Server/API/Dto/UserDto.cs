using System.ComponentModel.DataAnnotations;
namespace API.Dto
{
    public record CreateUserDto(
        [Required] string Name, 
        [EmailAddress] string Email,
        [Required] string Password
    );
    
    public record LoginUserDto(
        [Required] string Email, 
        [Required] string PassWord
    );
}