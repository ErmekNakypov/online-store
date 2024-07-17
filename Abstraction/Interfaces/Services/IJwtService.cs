using Model.Dtos.Identity;
using Model.Identity;

namespace Abstraction.Interfaces.Services;

public interface IJwtService
{
    AuthenticationResponse GenerateToken(ApplicationUser user);
}