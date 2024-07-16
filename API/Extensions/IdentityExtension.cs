using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model.Identity;

namespace API.Extensions;

public static class IdentityExtension
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();
    }
}