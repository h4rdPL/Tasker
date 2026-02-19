using Scalar.AspNetCore;
using Tasker.Application;
using Tasker.Application.Features.Users;
using Tasker.Application.Interfaces;
using Tasker.Domain.Interfaces;
using Tasker.Infrastructure;
using Tasker.Infrastructure.Repositories;
using Tasker.Infrastructure.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<RegisterUserHandler>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
