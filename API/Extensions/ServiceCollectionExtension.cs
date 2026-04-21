using goodburger_api.Application.Interfaces;
using goodburger_api.Application.Services;
using goodburger_api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace goodburger_api.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=goodburger.db";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IMenuCatalog, MenuCatalog>();

        return services;
    }
}