using Abstraction.Interfaces.Services;
using BLL.Services;

namespace API.Extensions;

public static class BllServicesServiceExtension
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddTransient<IJwtService, JwtService>();
    }
}   