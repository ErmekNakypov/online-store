using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Model.Dtos.Identity;


public class RegisterUserDto
{
    [Required(ErrorMessage = "Person Name can't be blank")]
    public string PersonName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email can't be blank")]
    [EmailAddress(ErrorMessage = "Email should be in proper format")]
    [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already registered")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number can't be blank")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should be in proper format")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password can't be blank")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm password can't be blank")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password should match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}