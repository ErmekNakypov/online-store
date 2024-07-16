using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Identity;
using Model.Identity;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationUser> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationUser> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> PostRegister(RegisterUserDto registerUserDto)
    {
        return null;
    }
}