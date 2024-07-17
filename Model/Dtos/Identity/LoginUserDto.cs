using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Model.Dtos.Identity;

public class LoginUserDto
{
    [Required(ErrorMessage = "Email can't be blank")]
    [EmailAddress(ErrorMessage = "Email should be in proper format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password can't be blank")]
    public string Password { get; set; } = string.Empty;

}