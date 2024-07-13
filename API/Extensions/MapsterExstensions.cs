using Mapster;
using Model.Dtos;
using Model.Entities;

namespace online_store.Extensions;

public static class MapsterExstensions
{
    public static void InitMapping(this IApplicationBuilder app)
    {
        app.UserMapping();
    }

    private static void UserMapping(this IApplicationBuilder app)
    {
        TypeAdapterConfig<User, UserDto>.NewConfig();
    }
}