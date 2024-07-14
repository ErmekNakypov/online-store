using Mapster;
using Model.Dtos.User;
using Model.Entities;

namespace API.Extensions;

public static class MapsterExtensions
{
    public static void InitMapping(this IApplicationBuilder app)
    {
        app.UserMapping();
    }

    private static void UserMapping(this IApplicationBuilder app)
    {
        TypeAdapterConfig<User, GetUserDto>.NewConfig();
    }
}