using Model.Dtos;

namespace Abstraction.Interfaces.Services;

public interface IUserService
{
    IQueryable<UserDto> GetUsers();
    Task<UserDto> GetUser(int id);
    Task<int> AddUser(UserDto userDto);
    Task<int> UpdateUser(UserDto userDto);
    IQueryable<UserDto> DeleteUser(int id);
}