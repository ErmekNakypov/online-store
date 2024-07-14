using API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => 
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration) 
        .ReadFrom.Services(services); 
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddBllServices();
builder.Services.AddRepositoryServices();
var app = builder.Build();
app.InitMapping();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

