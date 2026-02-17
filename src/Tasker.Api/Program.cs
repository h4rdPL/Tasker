using Scalar.AspNetCore;
using Tasker.Application;
using Tasker.Application.Features.Users;
using Tasker.Application.Interfaces;
using Tasker.Domain.Interfaces;
using Tasker.Infrastructure;
using Tasker.Infrastructure.Repositories;
using Tasker.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<RegisterUserHandler>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.MapControllers();

app.Run();
