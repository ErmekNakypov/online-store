using System.Net;
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
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<string> PostRegister(RegisterUserDto registerUserDto)
    {
      return await _accountService.RegisterUser(registerUserDto);
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<AuthenticationResponse> PostLogin(LoginUserDto loginUserDto)
    {
        return await _accountService.LoginUser(loginUserDto);
    }
    
    [HttpGet("logout")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task GetLogout()
    {
        await _accountService.LogoutUser();
       // HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
    }
    
    [HttpGet]
    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        return await _accountService.IsEmailAlreadyRegistered(email);
    }
}