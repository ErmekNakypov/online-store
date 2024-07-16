using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;

namespace Model.Dtos.Identity;

public class RegisterUserDto
{
    public string PersonName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string ConfirmPassword { get; set; } = String.Empty;
}