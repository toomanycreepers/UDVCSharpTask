using UssJuniorTest.Core.Services;
using UssJuniorTest.Infrastructure.Repositories;
using UssJuniorTest.Infrastructure.Store;

namespace UssJuniorTest;

public static class ServiceCollectionExtensions
{
    public static void RegisterAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IStore, InMemoryStore>();

        services.AddScoped<DriveLogService>();
        services.AddScoped<DriveLogRepository>();
        services.AddScoped<CarRepository>();
        services.AddScoped<PersonRepository>();
    }
}