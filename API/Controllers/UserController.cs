using System.Net;
using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("authors")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IQueryable<UserDto> GetUsers()
    {
        return _userService.GetUsers();
    }

    [HttpPost("add-user")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<int> AddUser(UserDto userDto)
    {
        return await _userService.AddUser(userDto);
    }
}