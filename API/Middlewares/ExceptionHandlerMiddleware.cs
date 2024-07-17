using System.Net;
using BLL.Exceptions;

namespace API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
            // if (context.Request.Path.StartsWithSegments("/api/account/logout") && context.Response.StatusCode == 200)
            // {
            //     context.Response.StatusCode = (int)HttpStatusCode.NoContent;
            // }
        }
        catch (NotFoundException ex)
        {
            _logger.LogError("{ExceptionType} {ExceptionMessage}",
                ex.GetType().ToString(), ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("{ExceptionType} {ExceptionMessage}", 
                ex.GetType().ToString(), ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("{ExceptionType} {ExceptionMessage}", 
                ex.GetType().ToString(), ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }
    
    // private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    // {
    //     _logger.LogError("{ExceptionType} {ExceptionMessage}",
    //         exception.GetType().ToString(), exception.Message);
    //     
    //     context.Response.ContentType = "application/json";
    //     context.Response.StatusCode = (int)statusCode;
    //     
    //     var result = JsonConvert.SerializeObject(new { error = exception.Message });
    //     await context.Response.WriteAsync(result);
    // }
}