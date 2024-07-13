using Abstraction.Interfaces.Repositories;
using Abstraction.Interfaces.Services;
using Mapster;
using Model.Dtos;
using Model.Entities;

namespace BLL.Services;

public class UserService : IUserService
{
    private IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public IQueryable<UserDto> GetUsers()
    {
        return _repository.GetItems().ProjectToType<UserDto>();
    }

    public async Task<UserDto> GetUser(int id)
    {
        var user = await _repository.GetItemAsync(x => x.Id == id)
                   ?? throw new ArgumentException("User not found");
        return user.Adapt<UserDto>();
    }

    public async Task<int> AddUser(UserDto userDto)
    {
        var user = userDto.Adapt<User>();
        user.CreatedAt = DateTime.Now;
        user = await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        return user.Id;
    }

    public async Task<int> UpdateUser(UserDto userDto)
    {
        var user = userDto.Adapt<User>();
        user.CreatedAt = DateTime.Now;
        user = _repository.UpdateItemAsync(user);
        await _repository.SaveChangesAsync();
        return user.Id;
    }

    public IQueryable<UserDto> DeleteUser(int id)
    {
        var users = _repository.DeleteItem(x => x.Id == id).ProjectToType<UserDto>()
            ?? throw new ArgumentException("User not found");
        _repository.SaveChangesAsync();
        return users;
    }
}