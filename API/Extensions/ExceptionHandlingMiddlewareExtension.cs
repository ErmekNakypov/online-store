using API.Middlewares;

namespace API.Extensions;

public static class ExceptionHandlingMiddlewareExtension
{
    public static void UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
         builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}