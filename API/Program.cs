

using online_store.Extensions;

var builder = WebApplication.CreateBuilder(args);


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

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

