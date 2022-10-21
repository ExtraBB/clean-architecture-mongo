using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Configuration;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetSection("Database"));

        ApplicationDbContext.RegisterClassMaps();

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
