using Microsoft.Extensions.DependencyInjection;
using Tasker.Application.Features.Tasks.CreateTask;

namespace Tasker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTaskHandler>();
        return services;
    }
}
