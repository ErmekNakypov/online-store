using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace online_store.Extensions;

public static class EntityFrameworkExtension
{
    public static void AddEntityFramework
        (this IServiceCollection services, IConfiguration configuration)
    {
         services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}