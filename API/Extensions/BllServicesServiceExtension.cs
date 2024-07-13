using Abstraction.Interfaces.Services;
using BLL.Services;

namespace online_store.Extensions;

public static class BllServicesServiceExtension
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}