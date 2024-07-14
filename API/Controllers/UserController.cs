using System.Net;
using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.User;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
        
    }

    [HttpGet("get-users")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<IQueryable<GetUserDto>>> GetUsers()
    {
        var users = await Task.FromResult(_userService.GetUsers());
        if (!users.Any()) return NoContent();
        return Ok(users);
    }

    [HttpGet("get-tracked-users")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<IQueryable<GetUserDto>>> GetTrackedUsers()
    {
        var users = await Task.FromResult(_userService.GetTrackedUsers());
        if (!users.Any()) return NoContent();
        return Ok(users);
    }

    [HttpGet("get-user/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetUserDto>> GetUser(int id)
    {
        var user = await _userService.GetUser(id);
        return Ok(user);
    }

    [HttpGet("get-tracked-user/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetUserDto>> GetTrackedUser(int id)
    {
        var user = await _userService.GetTrackedUser(id);
        return Ok(user);
    }
    
    [HttpPost("add-user")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<int>> AddUser(AddUserDto addUserDto)
    {
       return Ok(await _userService.AddUser(addUserDto));
    }

    [HttpPut("update-user")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<int>> UpdateUser(UpdateUserDto updateUserDto)
    {
        var userId = await _userService.UpdateUser(updateUserDto);
        return Ok(userId);
    }
    [HttpDelete("delete-user/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<int>> DeleteUser(int id)
    {
        var userId = await Task.FromResult(_userService.DeleteUser(id));
        return Ok(userId);
    }
}