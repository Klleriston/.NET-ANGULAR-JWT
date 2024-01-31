using api.Data;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<UserContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtServices>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(o => o
    .WithOrigins(new[] {"http://localhost:3000",} ) ///PORTA DO FRONTEND ANGULAS
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
