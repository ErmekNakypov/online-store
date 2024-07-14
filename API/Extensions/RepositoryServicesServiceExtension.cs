using Abstraction.Interfaces.Repositories;
using DAL.Repositories;

namespace API.Extensions;

public static class RepositoryServicesServiceExtension
{
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IUserRepository, UserRepository>();
    }
}