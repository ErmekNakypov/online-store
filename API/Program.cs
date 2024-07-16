using API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddBllServices();
builder.Services.AddRepositoryServices();
builder.Services.AddIdentity();
var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.InitMapping();
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

