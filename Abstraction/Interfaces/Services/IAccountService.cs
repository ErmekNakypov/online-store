using Model.Dtos.Identity;
using Model.Identity;

namespace Abstraction.Interfaces.Services;

public interface IAccountService
{
    Task<ApplicationUser> RegisterUser(RegisterUserDto registerUserDto);
    Task<bool> IsEmailAlreadyRegistered(string email);
    Task<ApplicationUser> LoginUser(LoginUserDto loginUserDto);
    Task LogoutUser();
}