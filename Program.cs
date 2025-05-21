using backend_jwt_auth.Data;
using backend_jwt_auth.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv; 

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"server={Environment.GetEnvironmentVariable("MYSQL_HOST")};" +
                       $"port={Environment.GetEnvironmentVariable("MYSQL_PORT")};" +
                       $"database={Environment.GetEnvironmentVariable("MYSQL_DB")};" +
                       $"user={Environment.GetEnvironmentVariable("MYSQL_USER")};" +
                       $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD")}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "fallback-key";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
    
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
