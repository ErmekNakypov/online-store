using Abstraction.Interfaces.Services;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Identity;
using Model.Identity;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ApplicationUser> PostRegister(RegisterUserDto registerUserDto)
    {
      return await _accountService.RegisterUser(registerUserDto);
    }

    [HttpGet]
    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        return await _accountService.IsEmailAlreadyRegistered(email);
    }
}