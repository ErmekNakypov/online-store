using Mapster;
using Model.Dtos.Identity;
using Model.Dtos.User;
using Model.Entities;
using Model.Identity;

namespace API.Extensions;

public static class MapsterExtensions
{
    public static void InitMapping(this IApplicationBuilder app)
    {
        app.ApplicationUserMapping();
        app.UserMapping();
    }

    private static void ApplicationUserMapping(this IApplicationBuilder app)
    {
        TypeAdapterConfig<RegisterUserDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);
    }
    private static void UserMapping(this IApplicationBuilder app)
    {
        TypeAdapterConfig<User, GetUserDto>.NewConfig();
    }
}