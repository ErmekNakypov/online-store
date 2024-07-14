using Serilog;

namespace API.Extensions;

public static class SerilogExtension
{
    public static void ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) => 
        {
            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) 
                .ReadFrom.Services(services); 
        });
    }
}