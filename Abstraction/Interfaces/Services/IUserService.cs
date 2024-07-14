using Model.Dtos.User;

namespace Abstraction.Interfaces.Services;

public interface IUserService
{
    IQueryable<GetUserDto> GetUsers();
    
    IQueryable<GetUserDto> GetTrackedUsers();
    
    Task<GetUserDto> GetUser(int id);
    Task<GetUserDto> GetTrackedUser(int id);
    Task<int> AddUser(AddUserDto getUserDto);
    Task<int> UpdateUser(UpdateUserDto getUserDto);
    IQueryable<GetUserDto> DeleteUser(int id);
}