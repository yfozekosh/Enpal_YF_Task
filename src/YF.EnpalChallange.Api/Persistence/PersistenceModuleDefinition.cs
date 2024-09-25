using System.Data;
using Dapper;
using Npgsql;
using YF.EnpalChallange.Api.Persistence.Abstract;

namespace YF.EnpalChallange.Api.Persistence;

public static class PersistenceModuleDefinition
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDb(services, configuration);
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        return services;
    }

    private static void ConfigureDb(IServiceCollection services, IConfiguration configuration)
    {
        // Add the database connection
        services.AddScoped<IDbConnection>(_ =>
            new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}