using Abstraction.Interfaces.Services;
using BLL.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Model.Dtos.Identity;
using Model.Identity;

namespace BLL.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> userManager, 
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = registerUserDto.Adapt<ApplicationUser>();
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded) throw new InvalidOperationException("Registration failed");
        await _signInManager.SignInAsync(user, isPersistent: false);
        return user;
    }

    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;
        return true;
    }

    public async Task<ApplicationUser> LoginUser(LoginUserDto loginUserDto)
    { 
        var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, 
           isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded) throw new InvalidOperationException("Login failed");
        var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
        if (user != null) return user;
        throw new NotFoundException($"$User with email {loginUserDto.Email} does not exist");
    }

    public async Task LogoutUser()
    {
        await _signInManager.SignOutAsync();
    }
}