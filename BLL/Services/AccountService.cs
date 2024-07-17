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
    private readonly IJwtService _jwtService;

    public AccountService(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<AuthenticationResponse> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = registerUserDto.Adapt<ApplicationUser>();
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Registration failed: {errors}");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        var authenticationResponse = _jwtService.GenerateToken(user);
        return authenticationResponse;
    }

    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }

    public async Task<AuthenticationResponse> LoginUser(LoginUserDto loginUserDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password,
            isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded) throw new InvalidOperationException("Invalid email/password");
        var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
        if (user is not null)   
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            var authenticationResponse = _jwtService.GenerateToken(user);
            return authenticationResponse;
        };
        
        throw new NotFoundException($"User with email {loginUserDto.Email} does not exist");
    }

    public async Task LogoutUser()
    {
        await _signInManager.SignOutAsync();
    }
}