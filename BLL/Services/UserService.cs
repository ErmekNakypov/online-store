using Abstraction.Interfaces.Repositories;
using Abstraction.Interfaces.Services;
using Mapster;
using Microsoft.Extensions.Logging;
using Model.Dtos.User;
using Model.Entities;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public IQueryable<GetUserDto> GetUsers()
    {
        _logger.LogInformation("Fetching all users");
        var users = _repository.GetItems().ProjectToType<GetUserDto>();
        _logger.LogInformation("Fetched {UserCount} users", users.Count());
        return users;
    }

    public IQueryable<GetUserDto> GetTrackedUsers()
    {
        _logger.LogInformation("Fetching all tracked users");
        var users = _repository.GetTrackedItems().ProjectToType<GetUserDto>();
        _logger.LogInformation("Fetched {UserCount} tracked users", users.Count());
        return users;
    }

    public async Task<GetUserDto> GetUser(int id)
    {
        _logger.LogInformation("Fetching user with ID {UserId}", id);
        var user = await _repository.GetItemAsync(x => x.Id == id);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", id);
            throw new ArgumentException("User not found");
        }
        _logger.LogInformation("Fetched user with ID {UserId}", id);
        return user.Adapt<GetUserDto>();
    }

    public async Task<GetUserDto> GetTrackedUser(int id)
    {
        _logger.LogInformation("Fetching tracked user with ID {UserId}", id);
        var user = await _repository.GetTrackedItemAsync(x => x.Id == id);
        if (user == null)
        {
            _logger.LogWarning("Tracked user with ID {UserId} not found", id);
            throw new ArgumentException("User not found");
        }
        _logger.LogInformation("Fetched tracked user with ID {UserId}", id);
        return user.Adapt<GetUserDto>();
    }

    public async Task<int> AddUser(AddUserDto addUserDto)
    {
        _logger.LogInformation("Adding new user with data {UserData}", addUserDto);
        var user = addUserDto.Adapt<User>();
        user.CreatedAt = DateTime.Now;
        user = await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Added new user with ID {UserId}", user.Id);
        return user.Id;
    }

    public async Task<int> UpdateUser(UpdateUserDto updateUserDto)
    {
        _logger.LogInformation("Updating user with data {UserData}", updateUserDto);
        
        var user = await _repository.GetItemAsync(x => x.Id == updateUserDto.Id);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", updateUserDto.Id);
            throw new ArgumentException("User not found");
        }
        
        user = updateUserDto.Adapt<User>();
        user.UpdatedAt = DateTime.Now;
        
        _repository.UpdateItemAsync(user);
        await _repository.SaveChangesAsync();
        
        _logger.LogInformation("Successfully updated user with ID {UserId}", user.Id);

        return user.Id;
    }

    public IQueryable<GetUserDto> DeleteUser(int id)
    {
        _logger.LogInformation("Deleting user with ID {UserId}", id);
        var user = _repository.GetItemAsync(x => x.Id == id);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", id);
            throw new ArgumentException("User not found");
        }
        var users = _repository.DeleteItem(x => x.Id == id).ProjectToType<GetUserDto>();
        _repository.SaveChangesAsync();
        _logger.LogInformation("Deleted user with ID {UserId}", id);
        return users;
    }
}