using Model.Dtos.Identity;
using Model.Identity;

namespace Abstraction.Interfaces.Services;

public interface IAccountService
{
    Task<string> RegisterUser(RegisterUserDto registerUserDto);
    Task<bool> IsEmailAlreadyRegistered(string email);
    Task<AuthenticationResponse> LoginUser(LoginUserDto loginUserDto);
    Task LogoutUser();
}