using System.ComponentModel.DataAnnotations;
namespace BackEnd.Dto
{
    public record CreateUserDto([Required] string Name, [EmailAddress] string Email,
                                [Required] string Password);
    public record LoginUserDto([Required] string Email, [Required] string PassWord);
}